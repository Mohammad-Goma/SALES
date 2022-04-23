using System;
using System.Collections;
using System.Windows.Forms;

namespace SALES
{
    public class ResizeTools
    {
        private Single WindowHeight;
        private Single WindowWidth;
        public Control Container
        {
            get;
            set;
        }
        public Hashtable RatioTable
        {
            get;
            set;
        }

        private struct SizeRatio
        {
            public Single TopRatio;
            public Single LeftRatio;
            public Single HeightRatio;
            public Single WidthRatio;
        };

        public void FullRatioTable()
        {
            WindowHeight = Container.Height;
            WindowWidth = Container.Width;
            RatioTable = new Hashtable();
            AddChildrenToTable(Container);
        }

        private void AddChildrenToTable(Control ChildContainer)
        {
            SizeRatio R = new SizeRatio();
            foreach (Control C in ChildContainer.Controls)
            {

                R.TopRatio = Convert.ToSingle(C.Top / WindowHeight);
                R.LeftRatio = Convert.ToSingle(C.Left / WindowWidth);
                R.HeightRatio = Convert.ToSingle(C.Height / WindowHeight);
                R.WidthRatio = Convert.ToSingle(C.Width / WindowWidth);
                RatioTable[C.Name] = R;
                if (C.HasChildren)
                {
                    AddChildrenToTable(C);
                }
            }
        }

        public void ResizeControls()
        {
            WindowHeight = Container.Height;
            WindowWidth = Container.Width;
            ResizeChildren(Container);
        }

        private void ResizeChildren(Control ChildContainer)
        {
            SizeRatio R = new SizeRatio();
            foreach (Control C in ChildContainer.Controls)
            {
                R = (ResizeTools.SizeRatio)RatioTable[C.Name];
                C.Top = Convert.ToInt16(WindowHeight * R.TopRatio);
                C.Left = Convert.ToInt16(WindowWidth * R.LeftRatio);
                C.Height = Convert.ToInt16(WindowHeight * R.HeightRatio);
                C.Width = Convert.ToInt16(WindowWidth * R.WidthRatio);

                if (C.HasChildren)
                {
                    ResizeChildren(C);
                }
            }
        }
    }
}
