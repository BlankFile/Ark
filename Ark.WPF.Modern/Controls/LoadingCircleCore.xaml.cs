using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Ark.WPF.Modern.Controls
{
    /// <summary>
    /// LoadingCircleCore.xaml の相互作用ロジック
    /// </summary>
    public partial class LoadingCircleCore : UserControl
    {
        /// <summary>
        /// LoadingCircleCore クラスの新しいインスタンスを初期化します。
        /// </summary>
        public LoadingCircleCore()
        {
            InitializeComponent();
        }

        #region 円を配置

        private const double CIRCLE_ANGLE = 36;
        private const double CIURCLE_RADIUS = 48;

        /// <summary>
        /// 初期処理。円を配置します。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInitialized(System.EventArgs e)
        {
            base.OnInitialized(e);

            var circleList = new List<Ellipse> 
            {
                circle1, circle2, circle3, circle4, circle5, 
                circle6, circle7, circle8, circle9, circle10 
            };

            var angleList = Enumerable.Range(1, circleList.Count).SelectToList(i => i * CIRCLE_ANGLE);

            circleList.Double(angleList, (circle, angle) =>
            {
                Canvas.SetLeft(circle, canvas.Width.Half() - circle.Width.Half() + GetX(angle));
                Canvas.SetBottom(circle, canvas.Height.Half() - circle.Height.Half() + GetY(angle));
            });
        }

        private double GetX(double angle)
        {
            return CIURCLE_RADIUS * Math.Cos(ConvertToRadian(angle));
        }

        private double GetY(double angle)
        {
            return CIURCLE_RADIUS * Math.Sin(ConvertToRadian(angle));
        }

        private double ConvertToRadian(double angle)
        {
            return angle * Math.PI / 180;
        }

        #endregion


    }
}
