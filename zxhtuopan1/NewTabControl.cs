using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace zxhtuopan1
{
    public class NewTabControl : TabControl
    {
        ///<summary>
        /// 构造函数,设置控件风格
        ///</summary>
        public NewTabControl()
        {
            SetStyle
                       (ControlStyles.AllPaintingInWmPaint  //全部在窗口绘制消息中绘图
                       | ControlStyles.OptimizedDoubleBuffer //使用双缓冲
                       , true);
        }
        ///<summary>
        /// 设置控件窗口创建参数的扩展风格
        ///</summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }
    }
}
