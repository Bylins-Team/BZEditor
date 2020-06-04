namespace BZEditor
{
    public class AutoCases
    {
        private readonly char[] buk0 = new char[13] {'б', 'в', 'д', 'з', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'ц'};
        private readonly char[] buk1 = new char[13] {'е', 'е', 'у', 'ю', 'у', 'а', 'у', 'у', 'у', 'у', 'у', 'у', 'у'};
        private readonly char[] buk2 = new char[13] {'б', 'в', 'д', 'з', 'л', 'м', 'м', 'п', 'р', 'с', 'м', 'ф', 'ц'};
        private readonly char[] ship = new char[4] {'ч', 'ш', 'щ', 'ц'};

        private readonly char[] sogl =
            new[]
                {
                    'б', 'в', 'г', 'д', 'ж', 'з', 'й', 'к', 'л', 'м', 'н', 'п', 'р', 'с', 'т', 'ф', 'х', 'ц', 'ч', 'ш', 'щ'
                };

        //private char[] zvon = new char[9] {'б', 'в', 'г', 'д', 'ж', 'з', 'н', 'р', 'ц'};

        private bool My(int ar, char bukv)
        {
            switch (ar)
            {
                case 0:
                    foreach (char c in buk0)
                    {
                        if (bukv == c)
                            return true;
                    }
                    break;
                case 1:
                    foreach (char c in buk1)
                    {
                        if (bukv == c)
                            return true;
                    }
                    break;
                case 2:
                    foreach (char c in buk2)
                    {
                        if (bukv == c)
                            return true;
                    }
                    break;
            }
            return false;
        }

        private bool Issogl(char bukv)
        {
            foreach (char c in sogl)
            {
                if (bukv == c)
                    return true;
            }
            return false;
        }

        private bool Isship(char bukv)
        {
            foreach (char c in ship)
            {
                if (bukv == c)
                    return true;
            }
            return false;
        }

        //private char lowc(char c)
        //{
        //    string s = c.ToString().ToLower();
        //    return s[0];
        //}

        //private string ruslat(string str, string newstr)
        //{
        //    //char[] latb = new char[] {"a","b","v","g","d","ye","zh","z","i","j","k","l","m","n","o","p","r","s","t","u","f","h","ts","ch","sh","sh","","y","","e","yu","ya"};
        //    //char clet;

        //    //Тут транслитерация
        //    /*newstr[0]='\0';

        //    for ( int i = 0 ; i <= strlen( str ) ; i++ ) 
        //    {
        //        clet = lowc( str[ i ] );
        //        if ( clet >= 'а' && clet <= 'п' ) 
        //            strcat( newstr, latb[ clet - 'а' ] );
        //        if ( clet >= 'р' && clet <= 'я' ) 
        //            strcat( newstr, latb[ clet - 'р' + 16 ] );
        //        if ( clet == 'ё' ) 
        //            strcat( newstr, "yo" );
        //    }
        //    if ( newstr[ 0 ] ==  '\0' )
        //        lstrcpy( newstr, str );*/
        //    return str;
        //}

        //Нахер ненужный бред
        //private bool isspace1(char c)
        //{
        //    if (c == ' ' || c == '\n' || c == '\r' || c == '\t' || c == 0)
        //        return true;
        //    return false;
        //}

        /// <summary>
        /// Тупо определение количества слов
        /// </summary>
        private int StrCount(string str)
        {
            string[] words = str.Split(new[] {' ', '\n', '\r', '\t'});
            return words.Length;
            /*int cnt=0, len, mlen = strin.Length;

            for (int i=0; i<mlen; i++)
            {
	            for (int len = 0; ( !isspace1( strin[i+len]) ) && ( i+len != mlen ); len++ )
                {
                }
	            if ( len > 0)
		            cnt++;
	            i += len;
            }
            return cnt;*/
        }

        /// <summary>
        /// Типо получение слова под номером num
        /// </summary>
        private string TakeWord(int num, string str)
        {
            string[] words = str.Split(new[] {' ', '\n', '\r', '\t'});
            if (num - 1 <= words.Length)
                return words[num - 1];
            else
                return "";
            /*
	        int svstr=0;
            int len;
            int mlen = stri.Length;

	        for (int i=0; i<mlen; i++)
            {
		        for ( len=0;(!isspace1(stri[i+len]))&&(i+len<mlen);len++ );
		        if ( len > 0 ) 
                {
			        if ( num == ( svstr + 1 ) )
                    {
				        for ( int j=i ; j <= i + len ; j++ ) 
					        newstr[ j - i ] = stri[ j ];
				        if (isspace1(stri[i+len]))
					        newstr[len]=0; 
				        else 
					        newstr[len+1]=0;
				        return;
			        }
			        i += len;
			        svstr++;
		        }
	        }
	        lstrcpy( newstr, "error" );*/
        }

        /// <summary>
        /// Получение количества значимых для парсинга слов
        /// </summary>
        private int StrCount1(string str)
        {
            string[] exclude =
                new[] {"для", "от", "из", "против", "на", "в", "с", "за", "под", "со", "про", "над"};
            int cnt = 0;
            string[] words = str.Split(new[] {' ', '\n', '\r', '\t'});
            foreach (string w in words)
            {
                bool exists = false;
                foreach (string exw in exclude)
                {
                    if (w == exw)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                    cnt++;
            }

            /*int len;
	        int mlen = (int)strlen(strin);
	        char * lstw = (char *) malloc( 2001 );

	        for (int i=0; i<mlen; i++) {
		        for (len=0;(!isspace1(strin[i+len]))&&(i+len!=mlen);len++);
		        if (len>0)
			        cnt++;
		        i+=len;
		        TakeWord( cnt, strin, lstw );
		        for (int j=0;j<=11;j++)
			        if ( !strcmp( lstw, exclude[j] ) && cnt > 1 ) {
				        free( lstw ); 
				        return ( cnt-1 ); 
			        }
	        }
	        free(lstw);*/

            return cnt;
        }

//        /// <summary>
//        /// Преобразование в нижний регистр с заменой Ё на е
//        /// </summary>
//        private string toLowWithE(string str)
//        {
//            return str.ToLower().Replace("ё", "е");
//            /*char up[] =   "АБВГДЕЁёЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ"; 
//            char down[] =   "абвгдееежзийклмнопрстуфхчцшщъыьэюя";
//
//            for ( int i = 0; i < (int) strlen( s ); i++ )
//                for ( int j = 0; j < (int) strlen( up ); j++ )
//                    if ( s[i] == up[j] )
//                        s[i] = down[j];*/
//        }

        private string ReplaceChar(string str, int idx, char chr)
        {
            string res = "";
            if (idx > 0)
                res += str.Substring(0, idx);
            res += chr;
            if (idx < str.Length - 1)
                res += str.Substring(idx, str.Length - idx - 1);
            return res;
        }

        /// <summary>
        /// Автоопределение пола
        /// </summary>
        /// <param name="iGender"></param>
        /// <param name="str"></param>
        /// <returns></returns>
        private int GenderDetermine(int iGender, string str)
        {
            if (str.Length > 1)
            {
                switch (str[str.Length - 1])
                {
                    case 'й':
                        return 0;
                    case 'о':
                        return 2;
                    case 'е':
                        return 2;
                    case 'ё':
                        return 2;
                }
            }

            return iGender;
        }

        /// <summary>
        /// Получение родительного падежа
        /// </summary>
        public string Rpad(string nslv, bool edChislo, int gender)
        {
            string slv;
            string reslv = string.Empty;
            int leng;
            int maxi = StrCount1(nslv);

            for (int i = 1; i <= StrCount(nslv); i++)
            {
                slv = TakeWord(i, nslv);
                gender = GenderDetermine(gender, slv);
                if (slv.Length > 1 && (i <= maxi))
                {
                    leng = slv.Length;
                    switch (slv[leng - 1])
                    {
                            // 1- ое склонение   ( + часть третьего)
                        case 'а':
                            if (!edChislo && !Issogl(slv[leng - 3]) && Issogl(slv[leng - 2]))
                            {
                                if (slv[leng - 3] == 'е')
                                {
                                    slv = ReplaceChar(slv, leng - 3, 'ё');
                                    slv.Remove(leng - 1);
                                    break;
                                }
                                if (slv[leng - 3] == 'о')
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'о');
                                    slv += "в";
                                    break;
                                }
                                slv.Remove(leng - 1);
                                break;
                            } //<гл><согл>а
                            if (!edChislo && Issogl(slv[leng - 3]) && Issogl(slv[leng - 2]))
                            {
                                if (slv[leng - 2] == 'д')
                                {
                                    slv.Remove(leng - 1);
                                    break;
                                }
                                slv = ReplaceChar(slv, leng - 1, slv[leng - 2]);
                                slv = ReplaceChar(slv, leng - 2, 'е');
                                break;
                            } //<согл><согл>а
                            if (My(0, slv[leng - 2]))
                                slv = ReplaceChar(slv, leng - 1, 'ы');
                            else
                                slv = ReplaceChar(slv, leng - 1, 'и');
                            break;
                        case 'я':
                            if (edChislo) // Единств. число -я
                            {
                                if (slv[leng - 2] == 'м')
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'е');
                                    slv += "ни";
                                } // проверка окончания "-мя"
                                else if (Isship(slv[leng - 3]) && slv[leng - 2] == 'а')
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'о');
                                    slv = ReplaceChar(slv, leng - 1, 'й');
                                }
                                else if (slv[leng - 2] == 'а' && i != StrCount(nslv))
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'о');
                                    slv = ReplaceChar(slv, leng - 1, 'й');
                                }
                                else if (slv[leng - 2] == 'я')
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'е');
                                    slv = ReplaceChar(slv, leng - 1, 'й');
                                }
                                else
                                    slv = ReplaceChar(slv, leng - 1, 'и');
                            }
                            else // Множественное число -я
                            {
                                if (slv[leng - 2] == 'ь')
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'е');
                                    slv += "в";
                                }
                                else if (Issogl(slv[leng - 2]))
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'е');
                                    slv += "й";
                                }
                                else if (slv[leng - 2] == 'и')
                                    //Тут было так, непонятно зачем,м.б. ошибка (slv[leng-2]='и')
                                    slv = ReplaceChar(slv, leng - 1, 'й');
                            }
                            break;
                            // 2- ое склонение
                        case 'й':
                            if (My(1, slv[leng - 2]))
                            {
                                slv = ReplaceChar(slv, leng - 1, 'я');
                                break;
                            }
                            if (slv[leng - 2] == 'и')
                            {
                                if (i != StrCount(nslv))
                                {
                                    if (slv[leng - 3] == 'к' || slv[leng - 3] == 'г')
                                    {
                                        slv = ReplaceChar(slv, leng - 2, 'о');
                                        slv = ReplaceChar(slv, leng - 1, 'г');
                                        slv += "о";
                                        break;
                                    }
                                    if (slv[leng - 3] == 'б' || slv[leng - 3] == 'з' || slv[leng - 3] == 'с')
                                    {
                                        slv = ReplaceChar(slv, leng - 2, 'ь');
                                        slv = ReplaceChar(slv, leng - 1, 'е');
                                        slv += "го";
                                        break;
                                    }
                                    {
                                        slv = ReplaceChar(slv, leng - 2, 'е');
                                        slv = ReplaceChar(slv, leng - 1, 'г');
                                        slv += "о";
                                        break;
                                    }
                                }
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'я');
                                    break;
                                } //-ий
                            }
                            if (slv[leng - 2] == 'о')
                            {
                                if (i == StrCount(nslv))
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'я');
                                    break;
                                }
                            }
                            if (slv[leng - 2] == 'ы')
                            {
                                if (i == StrCount(nslv))
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'о');
                                    slv = ReplaceChar(slv, leng - 1, 'г');
                                    slv += "о";
                                    break;
                                }
                            }
                            else
                            {
                                slv = ReplaceChar(slv, leng - 1, 'г');
                                slv += "о";
                                break;
                            } // -ой
                            if (slv[leng - 2] == 'ы')
                            {
                                if (i != StrCount(nslv))
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'о');
                                    slv = ReplaceChar(slv, leng - 1, 'г');
                                    slv += "о";
                                    break;
                                }
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'я');
                                    break;
                                } //-ый
                            }
                            slv = ReplaceChar(slv, leng - 1, 'и');
                            break;
                        case 'о':
                            if (Issogl(slv[leng - 2]))
                                slv = ReplaceChar(slv, leng - 1, 'а');
                            break;
                        case 'е':
                            if (i != StrCount(nslv)) //прилагательное
                            {
                                if (!edChislo)
                                    slv = ReplaceChar(slv, leng - 1, 'х');
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'г');
                                    slv += "о";
                                }
                            }
                            else
                            {
                                // существительное
                                if (Issogl(slv[leng - 2]) && !Isship(slv[leng - 2]))
                                    slv = ReplaceChar(slv, leng - 1, 'я');
                                else if (slv[leng - 2] == 'ь') //(slv[leng-2]='ь')
                                    slv = ReplaceChar(slv, leng - 1, 'я');
                                else
                                    slv = ReplaceChar(slv, leng - 1, 'я');
                            }
                            break;
                        case 'ё':
                            if (Issogl(slv[leng - 2]))
                                slv = ReplaceChar(slv, leng - 1, 'я');
                            else if (slv[leng - 2] == 'ь') //(slv[leng-2]='ь') 
                                slv = ReplaceChar(slv, leng - 1, 'я');
                            break;
                        case 'к':
                            if (slv[leng - 2] == 'о')
                            {
                                slv = ReplaceChar(slv, leng - 2, 'к');
                                slv = ReplaceChar(slv, leng - 1, 'а');
                            }
                            else if (slv[leng - 2] == 'е')
                            {
                                slv = ReplaceChar(slv, leng - 2, 'к');
                                slv = ReplaceChar(slv, leng - 1, 'а');
                            }
                            else if (slv[leng - 2] == 'ё' && leng > 3) // -ёк
                            {
                                if (Issogl(slv[leng - 3]))
                                {
                                    if (Issogl(slv[leng - 4]))
                                        slv += "а";
                                    else
                                    {
                                        slv = ReplaceChar(slv, leng - 2, 'ь');
                                        slv = ReplaceChar(slv, leng - 1, 'к');
                                        slv += "а";
                                    }
                                }
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'й');
                                    slv = ReplaceChar(slv, leng - 1, 'к');
                                    slv += "а";
                                }
                            }
                            else if (gender != 1)
                                slv += "а";
                            break;
                        case 'ц':
                            if (slv[leng - 2] == 'е' || slv[leng - 2] == 'я') // - ец  -яц
                            {
                                if (Issogl(slv[leng - 3])) // -<согл>ец <согл>яц
                                {
                                    if (Issogl(slv[leng - 4]))
                                        slv += "а";
                                    else
                                    {
                                        if (slv[leng - 3] == 'л')
                                        {
                                            slv = ReplaceChar(slv, leng - 2, 'ь');
                                            slv = ReplaceChar(slv, leng - 1, 'ц');
                                            slv += "а";
                                        }
                                        else
                                        {
                                            slv = ReplaceChar(slv, leng - 2, 'ц');
                                            slv = ReplaceChar(slv, leng - 1, 'а');
                                        }
                                    }
                                }
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'й');
                                    slv = ReplaceChar(slv, leng - 1, 'ц');
                                    slv += "а";
                                }
                            }
                            else
                                slv += "а";
                            break; // -лец
                            // 3- ье склонение
                        case 'ь':
                            if (slv[leng - 3] == 'е' && slv[leng - 2] == 'н')
                            {
                                if (gender != 1)
                                {
                                    slv = ReplaceChar(slv, leng - 3, 'н');
                                    slv = ReplaceChar(slv, leng - 2, 'я');
                                    slv.Remove(leng - 1);
                                }
                                else
                                    slv = ReplaceChar(slv, leng - 1, 'и');
                            }
                            else if (gender == 1)
                                slv = ReplaceChar(slv, leng - 1, 'и');
                            else
                                slv = ReplaceChar(slv, leng - 1, 'я');
                            break;
                        case 'в':
                            if (gender != 1)
                                slv += "а";
                            break;
                        case 'и':
                            if (!edChislo)
                            {
                                if (slv[leng - 2] == 'к') // - ки
                                {
                                    if (gender == 1)
                                    {
                                        slv = ReplaceChar(slv, leng - 2, 'о');
                                        slv = ReplaceChar(slv, leng - 1, 'к');
                                    }
                                    else
                                    {
                                        slv = ReplaceChar(slv, leng - 1, 'о');
                                        slv += "в";
                                    }
                                }
                                else // Мн. число -и
                                {
                                    if (Issogl(slv[leng - 2]))
                                    {
                                        slv = ReplaceChar(slv, leng - 1, 'е');
                                        slv += "й";
                                    }
                                    else
                                    {
                                        if (gender == 1)
                                            slv = ReplaceChar(slv, leng - 1, 'й');
                                        else
                                        {
                                            slv = ReplaceChar(slv, leng - 1, 'е');
                                            slv += "в";
                                        }
                                    }
                                }
                            }
                            break; // Несклоняемые или множественые
                        case 'ы':
                            if (!edChislo)
                            {
                                slv = ReplaceChar(slv, leng - 1, 'о');
                                slv += "в";
                            }
                            break;
                        case 'у':
                            break;
                        default:
                            if (gender != 1)
                                slv += "а"; // Разносклоняемые
                            break;
                    }
                    if (i == 1)
                        reslv = slv;
                    else
                        reslv += " " + slv;
                }
                else
                    reslv += " " + slv;
            }
            return reslv;
        }

        /// <summary>
        /// Получение родительного падежа
        /// </summary>
        public string Dpad(string nslv, bool edChislo, int gender)
        {
            string slv;
            string reslv = string.Empty;
            int leng;
            int maxi = StrCount1(nslv);

            for (int i = 1; i <= StrCount(nslv); i++)
            {
                slv = TakeWord(i, nslv);
                gender = GenderDetermine(gender, slv);
                if (slv.Length > 1 && (i <= maxi))
                {
                    leng = slv.Length;
                    switch (slv[leng - 1])
                    {
                            // 1- ое склонение   (+часть третьего)
                        case 'а':
                            if (!edChislo && !Issogl(slv[leng - 3]) && Issogl(slv[leng - 2]))
                            {
                                slv += "м";
                                break;
                            }
                            if (!edChislo)
                            {
                                slv += "м";
                                break;
                            }
                            else
                                slv = ReplaceChar(slv, leng - 1, 'е');
                            //slv[leng-1]='е'; 
                            break;
                        case 'я':
                            if (edChislo) // Ед. число
                            {
                                if (slv[leng - 2] == 'м')
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'е');
                                    //slv[leng-1]='е';
                                    slv += "ни";
                                } // проверка окончания "-мя" (ср.р.)
                                else if (Isship(slv[leng - 3]) && slv[leng - 2] == 'а')
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'о');
                                    //slv[leng-2]='о';
                                    slv = ReplaceChar(slv, leng - 1, 'й');
                                    //slv[leng-1]='й';
                                }
                                else if (slv[leng - 2] == 'а' && i != StrCount(nslv))
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'о');
                                    //slv[leng-2]='о';
                                    slv = ReplaceChar(slv, leng - 1, 'й');
                                    //slv[leng-1]='й';
                                }
                                else if (slv[leng - 2] == 'я')
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'е');
                                    //slv[leng-2]='е';
                                    slv = ReplaceChar(slv, leng - 1, 'й');
                                    //slv[leng-1]='й';
                                }
                                else if (slv[leng - 2] == 'и')
                                    slv = ReplaceChar(slv, leng - 1, 'и');
                                    //slv[leng-1]='и';
                                else
                                    slv = ReplaceChar(slv, leng - 1, 'е');
                                //slv[leng-1]='е'; 
                            }
                            else // Множественное число
                            {
                                slv = ReplaceChar(slv, leng - 1, 'я');
                                //slv[leng-1]='я';
                                slv += "м";
                            }

                            break;
                            // 2- ое склонение
                        case 'й':
                            if (My(1, slv[leng - 2]))
                            {
                                slv = ReplaceChar(slv, leng - 1, 'ю');
                                //slv[leng-1]='ю'; 
                                break;
                            }
                            if (slv[leng - 2] == 'и')
                            {
                                if (i != StrCount(nslv))
                                {
                                    if (slv[leng - 3] == 'к' || slv[leng - 3] == 'г')
                                    {
                                        slv = ReplaceChar(slv, leng - 2, 'о');
                                        //slv[leng-2]='о';
                                        slv = ReplaceChar(slv, leng - 1, 'м');
                                        //slv[leng-1]='м'; 
                                        slv += "у";
                                    }
                                    else if (slv[leng - 3] == 'б' || slv[leng - 3] == 'з' || slv[leng - 3] == 'с')
                                    {
                                        slv = ReplaceChar(slv, leng - 2, 'ь');
                                        //slv[leng-2]='ь';
                                        slv = ReplaceChar(slv, leng - 1, 'е');
                                        //slv[leng-1]='е'; 
                                        slv += "му";
                                    }
                                    else
                                    {
                                        slv = ReplaceChar(slv, leng - 2, 'е');
                                        //slv[leng-2]='е';
                                        slv = ReplaceChar(slv, leng - 1, 'м');
                                        //slv[leng-1]='м'; 
                                        slv += "у";
                                    }
                                    break;
                                }
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'ю');
                                    //slv[leng-1]='ю'; 
                                    break;
                                } //-ий
                            }
                            if (slv[leng - 2] == 'о')
                            {
                                if (i == StrCount(nslv))
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'ю');
                                    //slv[leng-1]='ю';
                                    break;
                                }
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'м');
                                    //slv[leng-1]='м'; 
                                    slv += "у";
                                    break;
                                }
                            } //-ой
                            if (slv[leng - 2] == 'ы')
                            {
                                if (i != StrCount(nslv))
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'о');
                                    //slv[leng-2]='о'; 
                                    slv = ReplaceChar(slv, leng - 1, 'м');
                                    //slv[leng-1]='м'; 
                                    slv += "у";
                                    break;
                                }
                            }
                            slv = ReplaceChar(slv, leng - 1, 'и');
                            //slv[leng-1]='и';				
                            break;
                        case 'о':
                            slv = ReplaceChar(slv, leng - 1, 'у');
                            //slv[leng-1]='у';
                            break;
                        case 'е':
                            if (i != StrCount(nslv)) //прилагательное
                            {
                                if (!edChislo) //(СР == 0)
                                    slv = ReplaceChar(slv, leng - 1, 'м');
                                    //slv[leng-1]='м'; 
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'м');
                                    //slv[leng-1]='м'; 
                                    slv += "у";
                                }
                            }
                            else
                            {
                                // существительные
                                if (Issogl(slv[leng - 2]) || slv[leng - 2] == 'ь')
                                {
                                    if (Isship(slv[leng - 2]))
                                        slv = ReplaceChar(slv, leng - 1, 'у');
                                        //slv[leng-1]='у'; 
                                    else
                                        slv = ReplaceChar(slv, leng - 1, 'ю');
                                    //slv[leng-1]='ю';
                                }
                                else
                                    slv = ReplaceChar(slv, leng - 1, 'ю');
                                //slv[leng-1]='ю';
                            }
                            break;
                        case 'ё':
                            if (Issogl(slv[leng - 2]) || slv[leng - 2] == 'ь')
                                slv = ReplaceChar(slv, leng - 1, 'ю');
                            //slv[leng-1]='ю';  
                            break;
                        case 'к':
                            if (slv[leng - 2] == 'о')
                            {
                                slv = ReplaceChar(slv, leng - 2, 'к');
                                //slv[leng-2]='к';
                                slv = ReplaceChar(slv, leng - 1, 'у');
                                //slv[leng-1]='у'; 
                            }
                            else if (slv[leng - 2] == 'е')
                            {
                                slv = ReplaceChar(slv, leng - 2, 'к');
                                //slv[leng-2]='к';
                                slv = ReplaceChar(slv, leng - 1, 'у');
                                //slv[leng-1]='у';
                            }
                            else if (slv[leng - 2] == 'ё' && leng > 3) // -ёк
                            {
                                if (Issogl(slv[leng - 3]))
                                {
                                    if (Issogl(slv[leng - 4]))
                                        slv += "у";
                                    else
                                    {
                                        slv = ReplaceChar(slv, leng - 2, 'ь');
                                        //slv[leng-2]='ь'; 
                                        slv = ReplaceChar(slv, leng - 1, 'к');
                                        //slv[leng-1]='к'; 
                                        slv += "у";
                                    }
                                }
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'й');
                                    //slv[leng-2]='й';
                                    slv = ReplaceChar(slv, leng - 1, 'к');
                                    //slv[leng-1]='к'; 
                                    slv += "у";
                                }
                            }
                            else if (gender != 1)
                                slv += "у";
                            break;
                        case 'ц':
                            if ((slv[leng - 2] == 'е' || slv[leng - 2] == 'я') && leng > 3)
                            {
                                // - ец  -яц
                                if (Issogl(slv[leng - 3]))
                                {
                                    // -<согл>ец <согл>яц
                                    if (Issogl(slv[leng - 4]))
                                        slv += "у";
                                    else
                                    {
                                        if (slv[leng - 3] == 'л')
                                        {
                                            slv = ReplaceChar(slv, leng - 2, 'ь');
                                            //slv[leng-2]='ь'; 
                                            slv = ReplaceChar(slv, leng - 1, 'ц');
                                            //slv[leng-1]='ц'; 
                                            slv += "у";
                                        }
                                        else
                                        {
                                            slv = ReplaceChar(slv, leng - 2, 'ц');
                                            //slv[leng-2]='ц'; 
                                            slv = ReplaceChar(slv, leng - 1, 'у');
                                            //slv[leng-1]='у';
                                        }
                                    }
                                }
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'й');
                                    //slv[leng-2]='й';
                                    slv = ReplaceChar(slv, leng - 1, 'ц');
                                    //slv[leng-1]='ц'; 
                                    slv += "у";
                                }
                            }
                            else
                                slv += "у";
                            break; // -лец
                            // 3- ье склонение
                        case 'ь':
                            if (slv[leng - 3] == 'е' && slv[leng - 2] == 'н')
                            {
                                if (gender != 1)
                                {
                                    slv = ReplaceChar(slv, leng - 3, 'н');
                                    //slv[leng-3]='н'; 
                                    slv = ReplaceChar(slv, leng - 2, 'ю');
                                    //slv[leng-2]='ю';
                                    slv.Remove(leng - 1);
                                }
                                else
                                    slv = ReplaceChar(slv, leng - 1, 'и');
                                //slv[leng-1]='и';
                            }
                            else if (gender == 1)
                            {
                                slv = ReplaceChar(slv, leng - 1, 'и');
                                //slv[leng-1]='и' ;
                            }
                            else
                            {
                                slv = ReplaceChar(slv, leng - 1, 'ю');
                                //slv[leng-1]='ю'; 
                            }
                            break;
                        case 'в':
                            if (gender != 1)
                                slv += "у";
                            break;
                        case 'и':
                            if (!edChislo)
                            {
                                if (Isship(slv[leng - 2]) || slv[leng - 2] == 'г' ||
                                    slv[leng - 2] == 'ж' || slv[leng - 2] == 'к' ||
                                    slv[leng - 2] == 'х')
                                    slv = ReplaceChar(slv, leng - 1, 'а');
                                else
                                    slv = ReplaceChar(slv, leng - 1, 'я');
                                
                                //if (!edChislo)
                                    slv += "м";
                                    //else // Непонятно когда должно выполняться это условие, но так, как оно расположено сейчас, оно вообще не выполнится
                                //    slv += "у";
                            }
                            break; // Несклоняемые или множественые
                        case 'ы':
                            if (!edChislo)
                            {
                                slv = ReplaceChar(slv, leng - 1, 'а');
                                //slv[leng-1]='а';
                                slv += "м";
                            }
                            break;
                        case 'у':
                            break;
                        default:
                            if (gender != 1)
                                slv += "у"; // Разносклоняемые и прочие
                            break;
                    }
                    if (i == 1)
                        reslv = slv;
                    else
                        reslv += " " + slv;
                }
                else
                    reslv += " " + slv;
            }
            return reslv;
        }

        /// <summary>
        /// Получение дательного падежа
        /// </summary>
        public string Vpad(string nslv, bool edChislo, bool odushevl, int gender)
        {
            string slv;
            string reslv = string.Empty;
            int leng;
            int maxi = StrCount1(nslv);

            for (int i = 1; i <= StrCount(nslv); i++)
            {
                slv = TakeWord(i, nslv);
                gender = GenderDetermine(gender, slv);
                if (slv.Length > 1 && (i <= maxi))
                {
                    leng = slv.Length;
                    if (odushevl && gender != 1)
                    {
                        /*Rpad( nslv, CH, out );
				        free( slv );
				        free( reslv );*/
                        return Rpad(nslv, edChislo, gender);
                    }
                    else
                    {
                        switch (slv[leng - 1])
                        {
                            case 'й':
                                if (My(1, slv[leng - 2]))
                                    slv = ReplaceChar(slv, leng - 1, 'я');
                                //slv[leng-1]='я';
                                break;
                            case 'а':
                                if (!edChislo)
                                {
                                }
                                else
                                    slv = ReplaceChar(slv, leng - 1, 'у');
                                //slv[leng-1]='у'; 
                                break;
                            case 'я':
                                if (slv[leng - 2] == 'а')
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'у');
                                    //slv[leng-2]='у';
                                    slv = ReplaceChar(slv, leng - 1, 'ю');
                                    //slv[leng-1]='ю'; 
                                }
                                else if ((Issogl(slv[leng - 2]) || slv[leng - 2] == 'ь') && edChislo)
                                    slv = ReplaceChar(slv, leng - 1, 'ю');
                                //slv[leng-1]='ю';
                                break;
                        }
                    }
                    if (i == 1)
                        reslv = slv;
                    else
                        reslv += " " + slv;
                }
                else
                    reslv += " " + slv;
            }
            return reslv;
        }

        /// <summary>
        /// Получение творительного падежа
        /// </summary>
        public string Tpad(string nslv, bool edChislo, int gender)
        {
            string slv;
            string reslv = string.Empty;
            int leng;
            int maxi = StrCount1(nslv);

            for (int i = 1; i <= StrCount(nslv); i++)
            {
                slv = TakeWord(i, nslv);
                gender = GenderDetermine(gender, slv);
                if (slv.Length > 1 && (i <= maxi))
                {
                    leng = slv.Length;
                    switch (slv[leng - 1])
                    {
                            // 1- ое склонение   (+часть третьего)
                        case 'а':
                            if (!edChislo)
                            {
                                slv += "ми";
                                break;
                            }
                            else
                            {
                                slv = ReplaceChar(slv, leng - 1, 'о');
                                //slv[leng - 1] = 'о'; 
                                slv += "й";
                            }
                            break;
                        case 'я':
                            if (edChislo)
                            {
                                //Ед.ч -я
                                if (slv[leng - 2] == 'м')
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'е');
                                    //slv[leng-1]='е';
                                    slv += "нем";
                                }
                                else if (i != StrCount(nslv) && slv[leng - 2] == 'а')
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'о');
                                    //slv[leng - 2] = 'о';
                                    slv = ReplaceChar(slv, leng - 1, 'й');
                                    //slv[leng - 1] = 'й';
                                }
                                else if (slv[leng - 2] == 'я')
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'е');
                                    //slv[leng - 2] = 'е';
                                    slv = ReplaceChar(slv, leng - 1, 'й');
                                    // slv[leng - 1] = 'й';
                                }
                                else if (Issogl(slv[leng - 3]))
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'ё');
                                    //slv[leng - 1] = 'ё';
                                    slv += "й";
                                }
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'е');
                                    //slv[leng - 1] = 'е';
                                    slv += "й";
                                }
                            }
                            else
                            {
                                //Мн. ч. -я
                                if (Issogl(slv[leng - 2]) || slv[leng - 2] == 'ь')
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'я');
                                    //slv[leng-1]='я';
                                    slv += "ми";
                                }
                            }
                            break;
                            // 2- ое склонение или прилаг
                        case 'й':
                            if (slv[leng - 2] == 'о' && i == StrCount(nslv))
                            {
                                //-ой сущ
                                slv = ReplaceChar(slv, leng - 1, 'е');
                                //slv[leng-1]='е';
                                slv += "м";
                                break;
                            }
                            if (slv[leng - 2] == 'и')
                            {
                                if (i != StrCount(nslv))
                                {
                                    // -ий прилаг
                                    if (slv[leng - 3] == 'г' || slv[leng - 3] == 'к' || slv[leng - 3] == 'ж')
                                    {
                                        slv = ReplaceChar(slv, leng - 2, 'и');
                                        //slv[leng-2]='и';
                                        slv = ReplaceChar(slv, leng - 1, 'м');
                                        //slv[leng-1]='м';
                                    }
                                    else if (slv[leng - 3] == 'б' || slv[leng - 3] == 'з' || slv[leng - 3] == 'с')
                                    {
                                        slv = ReplaceChar(slv, leng - 2, 'ь');
                                        //slv[leng-2]='ь';
                                        slv = ReplaceChar(slv, leng - 1, 'и');
                                        //slv[leng-1]='и';
                                        slv += "м";
                                    }
                                    else
                                    {
                                        slv = ReplaceChar(slv, leng - 2, 'е');
                                        //slv[leng-2]='е';
                                        slv = ReplaceChar(slv, leng - 1, 'м');
                                        //slv[leng-1]='м';
                                    }
                                }
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'е');
                                    //slv[leng-1]='е';
                                    slv += "м";
                                }
                                break;
                            }
                            if (slv[leng - 2] == 'о')
                            {
                                if (!Isship(slv[leng - 3]))
                                    slv = ReplaceChar(slv, leng - 2, 'ы'); 
                                    //slv[leng-2]='ы'; 
                                else
                                    slv = ReplaceChar(slv, leng - 2, 'и');
                                //slv[leng-2]='и';
                                slv = ReplaceChar(slv, leng - 1, 'м');
                                //slv[leng-1]='м'; 
                                break;
                            }
                            if (slv[leng - 2] == 'ы' || (slv[leng - 2] == 'и' && i != StrCount(nslv)))
                                slv = ReplaceChar(slv, leng - 1, 'м'); 
                                //slv[leng-1]='м';
                            else
                            {
                                slv = ReplaceChar(slv, leng - 1, 'е');
                                //slv[leng-1]='е'; 
                                slv += "м";
                            }
                            break;
                        case 'о':
                            slv += "м";
                            break;
                        case 'е':
                            if (i != StrCount(nslv))
                            {
                                //прилагательное
                                if (!edChislo)
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'м');
                                    //slv[leng-1]='м'; 
                                    slv += "и";
                                }
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'ы');
                                    //slv[leng-2]='ы';
                                    slv = ReplaceChar(slv, leng - 1, 'м');
                                    //slv[leng-1]='м';
                                }
                            }
                            else
                            {
                                //  существительное
                                slv += "м";
                            }
                            break;
                        case 'ё':
                            slv += "м";
                            break;
                        case 'к':
                            if (slv[leng - 2] == 'о')
                            {
                                slv = ReplaceChar(slv, leng - 2, 'к');
                                //slv[leng-2]='к';
                                slv = ReplaceChar(slv, leng - 1, 'о');
                                //slv[leng-1]='о'; 
                                slv += "м";
                            }
                            else if (slv[leng - 2] == 'е')
                            {
                                slv = ReplaceChar(slv, leng - 2, 'к');
                                //slv[leng-2]='к';
                                slv = ReplaceChar(slv, leng - 1, 'о');
                                //slv[leng-1]='о'; 
                                slv += "м";
                            }
                            else if (slv[leng - 2] == 'ё' && leng > 3) // -ёк
                            {
                                if (Issogl(slv[leng - 3]))
                                {
                                    if (Issogl(slv[leng - 4]))
                                    {
                                        slv = ReplaceChar(slv, leng - 2, 'ь');
                                        //slv[leng-2]='ь';
                                        slv = ReplaceChar(slv, leng - 1, 'к');
                                        //slv[leng-1]='к';
                                        slv += "ом";
                                    }
                                    else
                                    {
                                        slv = ReplaceChar(slv, leng - 2, 'ь');
                                        //slv[leng-2]='ь';
                                        slv = ReplaceChar(slv, leng - 1, 'к');
                                        //slv[leng-1]='к'; 
                                        slv += "ом";
                                    }
                                }
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'й');
                                    //slv[leng-2]='й';
                                    slv = ReplaceChar(slv, leng - 1, 'к');
                                    //slv[leng-1]='к'; 
                                    slv += "ом";
                                }
                            }
                            else if (gender != 1) //////?????????????????? stas
                                slv += "ом";
                            break;
                        case 'ц':
                            if ((slv[leng - 2] == 'е' || slv[leng - 2] == 'я') && leng > 3)
                            {
                                // - ец  -яц
                                if (Issogl(slv[leng - 3]))
                                {
                                    // -<согл>ец <согл>яц
                                    if (Issogl(slv[leng - 4]))
                                        slv += "ом";
                                    else
                                    {
                                        if (slv[leng - 3] == 'л')
                                        {
                                            slv = ReplaceChar(slv, leng - 2, 'ь');
                                            //slv[leng-2]='ь';
                                            slv = ReplaceChar(slv, leng - 1, 'ц');
                                            //slv[leng-1]='ц'; 
                                            slv += "ем";
                                        }
                                        else
                                        {
                                            slv = ReplaceChar(slv, leng - 2, 'ц');
                                            //slv[leng-2]='ц';
                                            slv = ReplaceChar(slv, leng - 1, 'о');
                                            //slv[leng-1]='о'; 
                                            slv += "м";
                                        }
                                    }
                                }
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'й');
                                    //slv[leng-2]='й';
                                    slv = ReplaceChar(slv, leng - 1, 'ц');
                                    //slv[leng-1]='ц'; 
                                    slv += "ем";
                                }
                            }
                            else
                                slv += "ем";
                            break; // -лец
                            // 3- ье склонение
                        case 'ь':
                            if (slv[leng - 3] == 'е' && slv[leng - 2] == 'н')
                            {
                                if (gender != 1)
                                {
                                    slv = ReplaceChar(slv, leng - 3, 'н');
                                    //slv[leng-3]='н';
                                    slv = ReplaceChar(slv, leng - 2, 'е');
                                    //slv[leng-2]='е';
                                    slv = ReplaceChar(slv, leng - 1, 'м');
                                    //slv[leng-1]='м';
                                }
                                else
                                    slv += "ю";
                                break;
                            }
                            else if (gender == 1)
                                slv += "ю";
                            else
                            {
                                slv = ReplaceChar(slv, leng - 1, 'е');
                                //slv[leng-1]='е';
                                slv += "м";
                            }
                            break;
                        case 'и':
                            if (!edChislo)
                            {
                                if ((Isship(slv[leng - 2])) || slv[leng - 2] == 'ж' || slv[leng - 2] == 'к' ||
                                    slv[leng - 2] == 'х')
                                    slv = ReplaceChar(slv, leng - 1, 'а');
                                //slv[leng-1]='а';
                                if ((Isship(slv[leng - 2])) || slv[leng - 2] == 'ж' || slv[leng - 2] == 'к' ||
                                    slv[leng - 2] == 'х')
                                    slv = ReplaceChar(slv, leng - 1, 'а');
                                    //slv[leng-1]='а';
                                else
                                    slv = ReplaceChar(slv, leng - 1, 'я');
                                //slv[leng-1]='я';
                                slv += "ми";
                            }
                            break; // Несклоняемые или множественые
                        case 'ы':
                            if (!edChislo)
                            {
                                slv = ReplaceChar(slv, leng - 1, 'а');
                                //slv[leng-1]='а';
                                slv += "ми";
                            }
                            break;
                        case 'у':
                            break;
                        default:
                            if (gender != 1)
                                slv += "ом"; // Разносклоняемые
                            break;
                    }
                    if (i == 1)
                        reslv = slv;
                    else
                        reslv += " " + slv;
                }
                else
                    reslv += " " + slv;
            }
            return reslv;
        }

/*void Tpad1( char * nslv, int CH, char * out ) {
//
//    Дополнительная (художественная или украинизированная)
//                 форма творительного падежа
//
	char * slv = (char *) malloc(2008);
	char * reslv = (char *) malloc(2009);
	int leng;
	int maxi=StrCount1(nslv);

	for (int i=1;i<=StrCount(nslv);i++) {
		TakeWord( i, nslv, slv ); 
		if (strlen(slv)>1 && (i<=maxi)) {
			leng=(int)strlen(slv);
			switch (slv[leng-1]) {
				case 'а':
						slv[leng-1]='о';
						strcat(slv,"ю");
						break;
				case 'я':
					if (issogl(slv[leng-2])) {
						slv[leng-1]='е';
						strcat(slv,"ю");
					}
					else
					if (slv[leng-2]=='а') {
						slv[leng-2]='о';
						slv[leng-1]='ю';
					}
				break;
				default : {}
			}
			if (i==1) lstrcpy( reslv, slv );
			else {
				strcat( reslv, " " );
				strcat( reslv, slv );
			}
		} else {
				strcat( reslv, " " );
				strcat( reslv, slv );
		}
	}
	lstrcpy( out, reslv );
	free(slv); 
	free(reslv);
}*/

        public string Ppad(string nslv, bool edChislo, int gender)
        {
            string slv;
            string reslv = string.Empty;
            int leng;
            int maxi = StrCount1(nslv);

            for (int i = 1; i <= StrCount(nslv); i++)
            {
                slv = TakeWord(i, nslv);
                gender = GenderDetermine(gender, slv);
                if (slv.Length > 1 && (i <= maxi))
                {
                    leng = slv.Length;
                    switch (slv[leng - 1])
                    {
                            // 1- ое склонение   (+часть третьего)
                        case 'а':
                            if (!edChislo)
                            {
                                slv += "х";
                                break;
                            }
                            else
                                slv = ReplaceChar(slv, leng - 1, 'е');
                            //slv[leng-1]='е'; 
                            break;
                        case 'я':
                            if (edChislo)
                            {
                                if (slv[leng - 2] == 'м')
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'е');
                                    //slv[leng-1]='е';
                                    slv += "ни";
                                }
                                else if (i != StrCount(nslv) && slv[leng - 2] == 'а')
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'о');
                                    //slv[leng-2]='о';
                                    slv = ReplaceChar(slv, leng - 1, 'й');
                                    //slv[leng-1]='й'; 
                                }
                                else if (slv[leng - 2] == 'я')
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'е');
                                    //slv[leng-2]='е';
                                    slv = ReplaceChar(slv, leng - 1, 'й');
                                    //slv[leng-1]='й'; 
                                }
                                else if (slv[leng - 2] == 'ь')
                                    slv = ReplaceChar(slv, leng - 1, 'е');
                                    //slv[leng-1]='е';
                                else if (slv[leng - 2] == 'и')
                                    slv = ReplaceChar(slv, leng - 1, 'и');
                                    //slv[leng-1]='и';
                                else if (Issogl(slv[leng - 2]) || slv[leng - 2] == 'ь')
                                {
                                    if (gender == 1)
                                        slv = ReplaceChar(slv, leng - 1, 'е');
                                        //slv[leng-1]='е';
                                    else
                                        slv = ReplaceChar(slv, leng - 1, 'и');
                                    //slv[leng-1]='и';
                                }
                                else
                                    slv = ReplaceChar(slv, leng - 2, 'е');
                                //slv[leng-2]='е'; 
                            }
                            else
                            {
                                if (Issogl(slv[leng - 2]) || slv[leng - 2] == 'ь')
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'я');
                                    //slv[leng-1]='я';
                                    slv += "х";
                                }
                            }
                            break;
                            // 2- ое склонение
                        case 'й':
                            if (i == StrCount(nslv))
                            {
                                //существительное
                                if (slv[leng - 2] == 'о')
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'е');
                                    //slv[leng-1]='е';
                                    break;
                                }
                                if (slv[leng - 2] == 'и')
                                {
                                    if (slv[leng - 3] == 'ш')
                                    {
                                        slv = ReplaceChar(slv, leng - 2, 'е');
                                        //slv[leng-2]='е';
                                        slv = ReplaceChar(slv, leng - 1, 'м');
                                        //slv[leng-1]='м';
                                    }
                                    else
                                        slv = ReplaceChar(slv, leng - 1, 'и');
                                    //slv[leng-1]='и';
                                } // прилагательное
                            }
                            else if (slv[leng - 2] == 'о')
                                slv = ReplaceChar(slv, leng - 1, 'м');
                                //slv[leng-1]='м';
                            else if (slv[leng - 2] == 'ы')
                            {
                                slv = ReplaceChar(slv, leng - 2, 'о');
                                //slv[leng-2]='о';
                                slv = ReplaceChar(slv, leng - 1, 'м');
                                //slv[leng-1]='м';
                            }
                            else if (slv[leng - 2] == 'и')
                            {
                                // -ий
                                if (slv[leng - 3] == 'н' || slv[leng - 3] == 'ж')
                                {
                                    // -ний -жий
                                    slv = ReplaceChar(slv, leng - 2, 'е');
                                    //slv[leng-2]='е';
                                    slv = ReplaceChar(slv, leng - 1, 'м');
                                    //slv[leng-1]='м';
                                }
                                else if (slv[leng - 3] == 'б' || slv[leng - 3] == 'з' || slv[leng - 3] == 'с')
                                {
                                    // -бий -зий -сий
                                    slv = ReplaceChar(slv, leng - 2, 'ь');
                                    //slv[leng-2]='ь';
                                    slv = ReplaceChar(slv, leng - 1, 'е');
                                    //slv[leng-1]='е';
                                    slv += "м";
                                }
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 1, 'о');
                                    //slv[leng-2]='о';
                                    slv = ReplaceChar(slv, leng - 1, 'м');
                                    //slv[leng-1]='м';
                                }
                            }
                            else
                                slv = ReplaceChar(slv, leng - 1, 'е');
                            //slv[leng-1]='е'; 
                            break;
                        case 'о':
                            slv = ReplaceChar(slv, leng - 1, 'е');
                            //slv[leng-1]='е'; 
                            break;
                        case 'е':
                            if (i != StrCount(nslv))
                            {
                                //прилагательное
                                if (edChislo)
                                    slv = ReplaceChar(slv, leng - 1, 'м');
                                    //slv[leng-1]='м'; 
                                else
                                    slv = ReplaceChar(slv, leng - 1, 'х');
                                //slv[leng-1]='х';
                            }
                            else
                            {
                                // Существительное
                                if (slv[leng - 2] == 'и')
                                    slv = ReplaceChar(slv, leng - 1, 'и');
                                //slv[leng-1]='и';
                            }
                            break;
                        case 'ё':
                            slv = ReplaceChar(slv, leng - 1, 'е');
                            //slv[leng-1]='е'; 
                            break;
                        case 'к':
                            if (slv[leng - 2] == 'о')
                            {
                                slv = ReplaceChar(slv, leng - 2, 'к');
                                //slv[leng-2]='к';
                                slv = ReplaceChar(slv, leng - 1, 'е');
                                //slv[leng-1]='е'; 
                                break;
                            }
                            if (slv[leng - 2] == 'е')
                            {
                                if (Isship(slv[leng - 3]))
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'к');
                                    //slv[leng-2]='к';
                                    slv = ReplaceChar(slv, leng - 1, 'е');
                                    //slv[leng-1]='е'; 
                                    break;
                                }
                                else
                                    slv += "а";
                                break;
                            }
                            if (slv[leng - 2] == 'ё')
                            {
                                // -ёк
                                if (Issogl(slv[leng - 3]))
                                {
                                    if (Issogl(slv[leng - 4]))
                                        slv += "е";
                                    else
                                    {
                                        slv = ReplaceChar(slv, leng - 2, 'ь');
                                        //slv[leng-2]='ь';
                                        slv = ReplaceChar(slv, leng - 1, 'к');
                                        //slv[leng-1]='к'; 
                                        slv += "е";
                                    }
                                }
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'й');
                                    //slv[leng-2]='й';
                                    slv = ReplaceChar(slv, leng - 1, 'к');
                                    //slv[leng-1]='к'; 
                                    slv += "е";
                                }
                                break;
                            }
                            if (gender != 1)
                                slv += "е";
                            break;
                        case 'ц':
                            if ((slv[leng - 2] == 'е' || slv[leng - 2] == 'я') && leng > 3)
                            {
                                // - ец  -яц
                                if (Issogl(slv[leng - 3]))
                                {
                                    // -<согл>ец <согл>яц
                                    if (Issogl(slv[leng - 4]))
                                        slv += "е";
                                    else
                                    {
                                        if (slv[leng - 3] == 'л')
                                        {
                                            slv = ReplaceChar(slv, leng - 2, 'ь');
                                            //slv[leng-2]='ь';
                                            slv = ReplaceChar(slv, leng - 1, 'ц');
                                            //slv[leng-1]='ц'; 
                                            slv += "е";
                                        }
                                        else
                                        {
                                            slv = ReplaceChar(slv, leng - 2, 'ц');
                                            //slv[leng-2]='ц';
                                            slv = ReplaceChar(slv, leng - 1, 'е');
                                            //slv[leng-1]='е';
                                        }
                                    }
                                }
                                else
                                {
                                    slv = ReplaceChar(slv, leng - 2, 'й');
                                    //slv[leng-2]='й';
                                    slv = ReplaceChar(slv, leng - 1, 'ц');
                                    //slv[leng-1]='ц'; 
                                    slv += "е";
                                }
                            }
                            else
                                slv += "е";
                            break; // -лец
                            // 3- ье склонение
                        case 'ь':
                            if (slv[leng - 3] == 'е' && slv[leng - 2] == 'н')
                            {
                                if (gender != 1)
                                {
                                    slv = ReplaceChar(slv, leng - 3, 'н');
                                    //slv[leng-3]='н';
                                    slv = ReplaceChar(slv, leng - 2, 'е');
                                    //slv[leng-2]='е';
                                    slv = ReplaceChar(slv, leng - 1, '\0');
                                    //slv[leng-1]='\0';
                                }
                                else
                                    slv = ReplaceChar(slv, leng - 1, 'и');
                                //slv[leng-1]='и';
                            }
                            else if (gender != 0)
                                slv = ReplaceChar(slv, leng - 1, 'и');
                                //slv[leng-1]='и'; 
                            else
                                slv = ReplaceChar(slv, leng - 1, 'е');
                            //slv[leng-1]='е';
                            break;
                        case 'и':
                            if (!edChislo)
                            {
                                if (slv[leng - 2] == 'г' || slv[leng - 2] == 'ж' || slv[leng - 2] == 'к' ||
                                    slv[leng - 2] == 'х' || slv[leng - 2] == 'ч' || slv[leng - 2] == 'ш' ||
                                    slv[leng - 2] == 'щ')
                                    slv = ReplaceChar(slv, leng - 1, 'а');
                                    //slv[leng-1]='а';
                                else
                                    slv = ReplaceChar(slv, leng - 1, 'я');
                                //slv[leng-1]='я';
                                slv += "х";
                            }
                            break; // Несклоняемые или множественые
                        case 'ы':
                            if (!edChislo)
                            {
                                slv = ReplaceChar(slv, leng - 1, 'а');
                                //slv[leng-1]='а';
                                slv += "х";
                            }
                            break;
                        case 'у':
                            break;
                        default:
                            if (gender != 1)
                                slv += "е"; // Разносклоняемые
                            break;
                    }
                    if (i == 1)
                        reslv = slv;
                    else
                        reslv += " " + slv;
                }
                else
                    reslv += " " + slv;
            }
            return reslv;
        }
    }
}