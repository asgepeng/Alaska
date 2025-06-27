using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Drawing2D;

namespace WinformApp.Data
{
    public class GradientMenuRenderer : ToolStripProfessionalRenderer
    {
        public Color FirstColor { get; set; } = Color.Blue;
        public Color SecondColor { get; set; } = Color.Gainsboro;
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected)
            {
                if (e.Item.IsOnDropDown)
                {
                    Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);
                    using (LinearGradientBrush brush = new LinearGradientBrush(rect, FirstColor, SecondColor, LinearGradientMode.Vertical))
                    {
                        e.Graphics.FillRectangle(new SolidBrush(SecondColor), rect);
                    }
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(FirstColor), new Rectangle(Point.Empty, e.Item.Size));
                }
            }
            else
            {
                if (e.Item.Pressed)
                {
                    e.Graphics.FillRectangle(new SolidBrush(FirstColor), new Rectangle(Point.Empty, e.Item.Size));
                }
            }
        }
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            if (e.ToolStrip is ToolStripDropDownMenu)
            {
                Rectangle rect = e.AffectedBounds;
                using (LinearGradientBrush brush = new LinearGradientBrush(rect, FirstColor, SecondColor, LinearGradientMode.Vertical))
                {
                    e.Graphics.FillRectangle(brush, rect);
                }
            }
            else
            {
                base.OnRenderToolStripBackground(e);
            }
        }
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        { // Prevent default border from rendering // Optionally, you can add custom border rendering here if needed }
        }
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {

            Rectangle rect = e.AffectedBounds;
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, FirstColor, SecondColor, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
        }
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, FirstColor, SecondColor, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
            // Optional: draw border if selected/pressed
            if (e.Item.Selected || e.Item.Pressed)
            {
                using (Pen pen = new Pen(Color.DarkGray))
                {
                    e.Graphics.DrawRectangle(pen, new Rectangle(0, 0, rect.Width - 1, rect.Height - 1));
                }
            }
        }
    }

    public class FlatRenderer : ToolStripProfessionalRenderer
    {
        public FlatRenderer() : base(new ProfessionalColorTable()) { }

        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            Rectangle rect = e.AffectedBounds;
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, Color.WhiteSmoke, Color.Silver, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
        }

        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            Rectangle rect = new Rectangle(Point.Empty, e.Item.Size);

            Color topColor = Color.WhiteSmoke;
            Color bottomColor = Color.Silver;

            if (e.Item.Pressed)
            {
                topColor = Color.Gainsboro;
                bottomColor = Color.Gray;
            }
            else if (e.Item.Selected)
            {
                topColor = Color.LightGray;
                bottomColor = Color.DarkGray;
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(rect, topColor, bottomColor, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
        }

        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            // Do nothing to remove 3D border
        }
    }


}
