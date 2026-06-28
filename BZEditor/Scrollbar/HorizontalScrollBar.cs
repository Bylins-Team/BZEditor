// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HorizontalScrollBar.cs" company="RAbit">
//   Copyright 2009 All Rights Reserved
// </copyright>
// <author>Alexander Dolgof</author>
// <email>dolgof@gmail.com</email>
// <summary>
//   Реализует горизонтальную полосу прокрутки.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace BZEditor
{
    #region using

    using System.Windows.Forms;

    #endregion

    /// <summary>
    /// Реализует горизонтальную полосу прокрутки.
    /// </summary>
    public class HorizontalScrollBar : HScrollBar
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HorizontalScrollBar"/> class.
        /// </summary>
        public HorizontalScrollBar()
        {
            this.InitializeComponent();
            this.Reset();
        }

        #endregion

        #region Delegates

        private delegate void SetMaximumEventHandler(int newValue, int смещениеПолзуна);

        private delegate void SetScrollPositionEventHandler(int newValue);

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает или задает минимальное значение для полосы прокрутки.
        /// </summary>
        /// <value>
        /// Минимальное значение для полосы прокрутки.
        /// </value>
        public new int Minimum
        {
            get
            {
                return base.Minimum;
            }

            set
            {
                if (Maximum < value)
                {
                    Maximum = value;
                }

                // В исходнике .Net Framework v2.0.40607 вместо
                // "this.Value = value" написано "this.value = value",
                // поэтому OnValueChanged не вызывается.
                // Здесь эта ошибка обходится.
                if (value > Value)
                {
                    Value = value;
                }

                base.Minimum = value;
            }
        }

        /// <summary>
        /// Возвращает или задает видимость для прокрутки.
        /// </summary>
        /// <value>
        /// Видимость для прокрутки.
        /// </value>
        public new bool Visible
        {
            get
            {
                return base.Visible;
            }

            set
            {
                if (base.Visible == value)
                {
                    return;
                }

                base.Visible = value;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Сброс максимального и минимального значений полосы прокрутки.
        /// </summary>
        public void Reset()
        {
            this.Minimum = this.Maximum = 0;
        }

        /// <summary>
        /// Устанавливает максимальное значение для полосы прокрутки.
        /// </summary>
        /// <param name="newValue">
        /// Максимальное значение для полосы прокрутки.
        /// </param>
        /// <param name="scrollerOffset">
        /// Смещение ползунка.
        /// </param>
        public void SetMaximum(int newValue, int scrollerOffset)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetMaximumEventHandler(this.SetMaximum), new object[] { newValue, scrollerOffset });
            }
            else
            {
                this.Maximum = newValue;
            }
        }

        /// <summary>
        /// Устанавливает позицию ползунка.
        /// </summary>
        /// <param name="newValue">
        /// Позиция ползунка.
        /// </param>
        public void SetScrollPosition(int newValue)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetScrollPositionEventHandler(this.SetScrollPosition), new object[] { newValue });
            }
            else
            {
                this.Value = newValue;
            }
        }

        #endregion

        #region Methods

        private void InitializeComponent()
        {
            this.Cursor = Cursors.Arrow;
            this.Dock = DockStyle.Bottom;
            this.SmallChange = this.LargeChange = 1;
        }

        #endregion
    }
}