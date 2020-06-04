namespace DataUtils
{
    public class Shop : BaseDataObject
    {
        public BaseDataArrayList BuyingObjectsList = new BaseDataArrayList();
        public BaseDataArrayList ChangingObjectsList = new BaseDataArrayList();
        private string _bitvector = "";
        private decimal _buyCoeff = 0.33M;
        private decimal _changeCoeff = 0.33M;
        private int _closingTime1 = 28;
        private int _closingTime2 = 28;
        private int _emotion;
        private string _msg1 = "%s Этого нет в списке товаров!";
        private string _msg2 = "%s Извините, я это не покупаю!";
        private string _msg3 = "%s Извините, я это не покупаю!";
        private string _msg4 = "%s Извините, но сейчас у меня нет денег!";
        private string _msg5 = "%s У вас нет столько денег!";
        private string _msg6 = "%s Это будет стоить %d.";
        private string _msg7 = "%s Теперь у Вас есть %d!";
        private string _notTradeWithBitvector = "";
        private int _openingTime1;
        private int _openingTime2;
        private decimal _sellCoeff = 1;
        private int _shopkeeperVNum = -1;
        public BaseDataArrayList PermanentlySellingList = new BaseDataArrayList();
        public BaseDataArrayList ShopLocationsList = new BaseDataArrayList();

        public Shop(int vNum)
        {
            VNum = vNum;
            PermanentlySellingList.Changed += FireChangeEvent;
            BuyingObjectsList.Changed += FireChangeEvent;
            ChangingObjectsList.Changed += FireChangeEvent;
            ShopLocationsList.Changed += FireChangeEvent;
        }

        /// <summary>
        /// Процент при продаже
        /// </summary>
        public decimal SellCoeff
        {
            get { return _sellCoeff; }
            set
            {
                if (_sellCoeff == value) return;
                _sellCoeff = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Процент при покупке
        /// </summary>
        public decimal BuyCoeff
        {
            get { return _buyCoeff; }
            set
            {
                if (_buyCoeff == value) return;
                _buyCoeff = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Процент при обмене
        /// </summary>
        public decimal ChangeCoeff
        {
            get { return _changeCoeff; }
            set
            {
                if (_changeCoeff == value) return;
                _changeCoeff = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Сооющение 1
        /// </summary>
        public string Msg1
        {
            get { return _msg1; }
            set
            {
                if (_msg1 == value) return;
                _msg1 = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Сооющение 2
        /// </summary>
        public string Msg2
        {
            get { return _msg2; }
            set
            {
                if (_msg2 == value) return;
                _msg2 = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Сооющение 3
        /// </summary>
        public string Msg3
        {
            get { return _msg3; }
            set
            {
                if (_msg3 == value) return;
                _msg3 = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Сооющение 4
        /// </summary>
        public string Msg4
        {
            get { return _msg4; }
            set
            {
                if (_msg4 == value) return;
                _msg4 = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Сооющение 5
        /// </summary>
        public string Msg5
        {
            get { return _msg5; }
            set
            {
                if (_msg5 == value) return;
                _msg5 = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Сооющение 6
        /// </summary>
        public string Msg6
        {
            get { return _msg6; }
            set
            {
                if (_msg6 == value) return;
                _msg6 = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Сооющение 7
        /// </summary>
        public string Msg7
        {
            get { return _msg7; }
            set
            {
                if (_msg7 == value) return;
                _msg7 = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Тип реакции моба продавца
        /// </summary>
        public int Emotion
        {
            get { return _emotion; }
            set
            {
                if (_emotion == value) return;
                _emotion = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Битвектор
        /// </summary>
        public string Bitvector
        {
            get { return _bitvector; }
            set
            {
                if (_bitvector == value) return;
                _bitvector = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Список флагов определяющий с кем не торгует продавец
        /// </summary>
        public string NotTradeWithBitvector
        {
            get { return _notTradeWithBitvector; }
            set
            {
                if (_notTradeWithBitvector == value) return;
                _notTradeWithBitvector = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Виртуальный номер продавца
        /// </summary>
        public int ShopkeeperVNum
        {
            get { return _shopkeeperVNum; }
            set
            {
                if (_shopkeeperVNum == value) return;
                _shopkeeperVNum = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Время открытия магазина до обеда
        /// </summary>
        public int OpeningTime1
        {
            get { return _openingTime1; }
            set
            {
                if (_openingTime1 == value) return;
                _openingTime1 = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Время закрытия магазина на обед
        /// </summary>
        public int ClosingTime1
        {
            get { return _closingTime1; }
            set
            {
                if (_closingTime1 == value) return;
                _closingTime1 = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Время открытия магазина после обеда
        /// </summary>
        public int OpeningTime2
        {
            get { return _openingTime2; }
            set
            {
                if (_openingTime2 == value) return;
                _openingTime2 = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// Время закрытия магазина на ночь
        /// </summary>
        public int ClosingTime2
        {
            get { return _closingTime2; }
            set
            {
                if (_closingTime2 == value) return;
                _closingTime2 = value;
                FireChangeEvent(this);
            }
        }

        public void AddPermanentlySellingObject(int vNum)
        {
            PermanentlySellingList.Add(vNum);
        }

        public void AddBuyingObject(string vNum)
        {
            BuyingObjectsList.Add(vNum);
        }

        public void AddChangingObject(string vNum)
        {
            ChangingObjectsList.Add(vNum);
        }

        public void AddShopLocation(int vNum)
        {
            ShopLocationsList.Add(vNum);
        }
    }
}