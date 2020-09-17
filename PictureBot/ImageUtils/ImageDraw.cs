using PictureBot.InputUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace PictureBot.ImageUtils
{
    public class ImageRead
    {

        public delegate void ReadedEvent(Dictionary<string, List<int[]>> ImageMap);
        public event ReadedEvent onImageReaded;

        List<Thread> runningThreads;
        List<object> readedData;

        int currentThreads = 0;

        public void ReadFullImage(Image Target, int JumpH, int JumpV, int MinimiumColorDifference, bool NoWhite)
        {
            runningThreads = new List<Thread>();
            readedData = new List<object>();

            //create read image thread
            runningThreads.Add(new Thread(() => ReadData(Target, new[] { 0, 0, Target.Width, Target.Height}, JumpH, JumpV, MinimiumColorDifference, NoWhite, true)) { IsBackground = true });

            Debug.WriteLine("Starting Threads...");
            currentThreads = 1;
            //start read thread
            foreach(Thread t in runningThreads)
            {
                t.Priority = ThreadPriority.Highest;
                t.Start();
            }
        }
        private void ReadData(Image img, int[] StartEnd, int JumpX, int JumpY, int MinColorDiff, bool NoWhite, bool Wait)
        {
            //get image data to read faster
            ImageFast fast = new ImageFast(new Bitmap(img));
            fast.LockBits();

            //create dictionary with x/y coordinates and RGB colors
            Dictionary<string, List<int[]>> map = new Dictionary<string, List<int[]>>();
            List<string> ColorHistory = new List<string>();
            int[] lastRGB = { 0, 0, 0 };

            //start reading image
            for(int x=StartEnd[0]; x < StartEnd[2]; x+=JumpX)
            {
                for(int y=StartEnd[1]; y < StartEnd[3]; y+=JumpY)
                {
                    //get pixel rgb and check if is white
                    int[] newRGB = fast.GetPixel(x, y);
                    if (newRGB[0] > 230 && newRGB[1] > 230 && newRGB[2] > 230 && NoWhite) continue;

                    lastRGB = newRGB;
                    int lastDiff = 999999;
                    foreach(string color in ColorHistory)
                    {
                        //check if color is in history
                        int diff = RGBdiff(GetRGBFromString(color), newRGB);
                        if(diff < MinColorDiff && diff < lastDiff)
                        if(diff < MinColorDiff && diff < lastDiff)
                        {
                            //if color is in history, change color to match
                            lastDiff = diff;
                            lastRGB = GetRGBFromString(color);
                        }
                    }

                    //add coordinates to current color
                    List<int[]> cords = new List<int[]>();
                    if(map.ContainsKey(GetStringFromRGB(lastRGB)))
                    {
                        cords = map[GetStringFromRGB(lastRGB)];
                        cords.Add(new[] { x, y });
                        map[GetStringFromRGB(lastRGB)] = cords;
                    } else
                    {
                        ColorHistory.Add(GetStringFromRGB(lastRGB));
                        map.Add(GetStringFromRGB(lastRGB), cords);
                    }
                }
            }
            //end image read
            readedData.Add(map);
            Debug.WriteLine(string.Format("Thread {0} stopped.", currentThreads));
            currentThreads -= 1;

            while(Wait)
            {
                if(currentThreads <= 0)
                {
                    ReadEnded();
                    break;
                }
            }
        }

        private void ReadEnded()
        {
            //sort rgb colors, that changes the current color only 1 time for each color
            Debug.WriteLine("Image Readed, Parsing data...");
            SortedDictionary<string, List<int[]>> map = new SortedDictionary<string, List<int[]>>();
            foreach(object img in readedData)
            {
                Dictionary<string, List<int[]>> imageMap = (Dictionary<string, List<int[]>>)img;
                foreach(string key in imageMap.Keys)
                {
                    string rgb = key;

                    if(map.Keys.Contains(rgb))
                    {
                        List<int[]> cord = map[rgb];
                        foreach (int[] xy in imageMap[key]) cord.Add(xy);
                        map[rgb] = cord;
                    } else
                    {
                        List<int[]> cord = new List<int[]>();
                        foreach (int[] xy in imageMap[key]) cord.Add(xy);
                        map.Add(rgb, cord);
                    }
                }
            }
            //raise event
            Debug.WriteLine("Success !\nWaiting for user...");
            onImageReaded(new Dictionary<string, List<int[]>>(map));
        }

        public void StopReading()
        {
            //abort reading
            currentThreads = 0;
            foreach(Thread t in runningThreads)
            {
                t.Abort();
            }
            Debug.WriteLine("Threads stopped.");
        }

        public static string GetStringFromRGB(int[] RGB)
        {
            return string.Format("{0};{1};{2}", RGB[0], RGB[1], RGB[2]);
        }

        public static int[] GetRGBFromString(string RGB)
        {
            int[] newRGB = new int[3];
            string[] data = RGB.Split(';');

            newRGB[0] = int.Parse(data[0]);
            newRGB[1] = int.Parse(data[1]);
            newRGB[2] = int.Parse(data[2]);
            return newRGB;
        }

        public static int RGBdiff(int[] RGB1, int[] RGB2)
        {
            var diff = Math.Abs(RGB1[0] - RGB2[0]) + Math.Abs(RGB1[1] - RGB2[1]) + Math.Abs(RGB1[2] - RGB2[2]);
            return diff;
        }
    }

    public class ImageDraw
    {
        public static bool IsPaused { get; set; }
        public static bool IsRunning { get; private set; }

        static Dictionary<string, List<int[]>> image;
        static int MJump;
        static int TSleep;
        static Point selectColor = new Point();
        static Point colorR = new Point();
        static Point colorG = new Point();
        static Point colorB = new Point();

        public delegate void DrawEndEvent(bool Success);
        public static event DrawEndEvent onDrawEnd;

        public static void StartDrawing(Dictionary<string, List<int[]>> ImageData, int PixelJump, int ThreadSleep)
        {
            //load data
            IsRunning = true;
            IsPaused = false;
            image = ImageData;
            MJump = PixelJump;
            TSleep = ThreadSleep;
            selectColor = Properties.Settings.Default.ColorLocation;
            colorR = Properties.Settings.Default.ColorR;
            colorG = Properties.Settings.Default.ColorG;
            colorB = Properties.Settings.Default.ColorB;

            //run thread
            Debug.WriteLine("Starting draw thread...");
            new Thread(new ThreadStart(DrawThread)) { IsBackground = true }.Start();
        }

        public static void StopDrawing()
        {
            //stop drawing
            IsRunning = false;
            try
            {
                onDrawEnd(false);
            }
            catch { }
        }

        static int[] currentRGB;

        private static void DrawThread()
        {
            int currentX = Properties.Settings.Default.CanvasStart.X;
            int currentY = Properties.Settings.Default.CanvasStart.Y;

            Debug.WriteLine("Drawing image...");
            currentRGB = new int[3];
            //start selecting pen
            Cursor.Position = Properties.Settings.Default.PenLocation;
            MouseInput.simulator.Mouse.LeftButtonClick();
            Thread.Sleep(TSleep);

            bool holding = false;
            foreach (string color in image.Keys)
            {
                //read colors and change it
                if (!IsRunning) break;
                if (holding) { MouseInput.simulator.Mouse.LeftButtonUp(); holding = false; }
                if (ImageRead.GetStringFromRGB(currentRGB) != color) ChangeCurrentColor(ImageRead.GetRGBFromString(color));

                List<int[]> positions = new List<int[]>();
                positions = image[color];

                for (int i=0; i < positions.Count; i++)
                {
                    while(IsPaused)
                    {
                        if (!IsRunning) break;
                    }
                    //get all coordinates for current color
                    int[] position = positions[i];

                    if (!IsRunning) break;
                    //hold mouse cursor to draw a line
                    if(!holding) { Cursor.Position = new Point(currentX + position[0], currentY + position[1]); MouseInput.simulator.Mouse.LeftButtonDown(); holding = true; }
                    try
                    {
                        int[] next = positions[i + 1];
                        int xD = Math.Abs(position[0] - next[0]);
                        int yD = Math.Abs(position[1] - next[1]);
                        if (xD > 0 || yD > MJump) 
                        {
                            //set cursor to final position for current X, release mouse button and goto next
                            Cursor.Position = new Point(currentX + position[0], currentY + position[1]);
                            Thread.Sleep(TSleep);
                            MouseInput.simulator.Mouse.LeftButtonUp(); 
                            holding = false;
                            Thread.Sleep(TimeSpan.FromMilliseconds(TSleep));
                        }
                    } catch { break; }
                }
            }

            //raise event
            string result = IsRunning ? "Sucess" : "Stopped by user";
            Debug.WriteLine("Draw ended\nResult: " + result);

            if (holding) MouseInput.simulator.Mouse.LeftButtonUp();
            onDrawEnd(IsRunning);
            IsRunning = false;
        }

        private static void ChangeCurrentColor(int[] newColor)
        {
            //click on color selector
            Cursor.Position = selectColor;
            MouseInput.simulator.Mouse.LeftButtonClick();
            Thread.Sleep(600);

            //change R
            Cursor.Position = colorR;
            MouseInput.simulator.Mouse.LeftButtonDoubleClick();
            SendKeys.SendWait(newColor[0].ToString());
            Thread.Sleep(50);

            //change G
            Cursor.Position = colorG;
            MouseInput.simulator.Mouse.LeftButtonDoubleClick();
            SendKeys.SendWait(newColor[1].ToString());
            Thread.Sleep(50);

            //change B
            Cursor.Position = colorB;
            MouseInput.simulator.Mouse.LeftButtonDoubleClick();
            Thread.Sleep(50);
            SendKeys.SendWait(newColor[2].ToString());
            Thread.Sleep(50);

            currentRGB[0] = newColor[0];
            currentRGB[1] = newColor[1];
            currentRGB[2] = newColor[2];

            //close window
            SendKeys.SendWait("{ENTER}");
            Thread.Sleep(50);
        }
    }
}
