using System;

namespace BZEditor
{
    internal class TextFormater
    {
        private bool allowHyp = true;
        private readonly int maxLength = 70;
        private const string RusLetters = "АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        public TextFormater()
        {
        }

        public TextFormater(int maxLength)
        {
            this.maxLength = maxLength;
        }

        public string GetFormatedText(string text, bool asOneParagr, bool allowHyp, bool insertSpaces)
        {
            //ToDo: в бруске форматирование сливает все в кучу, и потом выравнивает, а потому оно теряет абзацы и переносы на следующую строку
            //ToDo: а надо сделать форматирование для каждого абзаца и возможно опционально сливать все в один абзац
            this.allowHyp = allowHyp;
            string resText = string.Empty;
            if (!asOneParagr)
            {
                string[] lines = text.Replace("\r", "").Split('\n');
                bool flag = true;
                foreach (string line in lines)
                {
                    if (resText.Length > 0)
                        resText += "\r\n";
                    resText += FormatString(line, flag, insertSpaces);
                    flag = false;
                }
            }
            else
            {
                resText = FormatString(text.Replace("\r", "").Replace("\n", " "), true, insertSpaces);
                //для гарантии совместимости с линуксовым переводом строки
            }
            return resText;
        }

        private string FormatString(string line, bool flag, bool insertSpaces)
        {
            /*Line = Line.TrimEnd(new char[] { ' ' });//  Удаляем концевые пробелы
            string tmps = Line;
            Line = Line.TrimStart(new char[] { ' ' });
            */
            //string Text = tmps.Replace(Line, "");
            string res = "";
            line = line.Trim();
            string text = "";
            if (flag) text += "   "; //волшебные три пробела в первой строке
            int cnt = 0;
            foreach (string word in line.Split(new [] {' '}))
            {
                if (text.Length + word.Length < maxLength)
                {
                    if (text.Length > 0)
                        if (text[text.Length - 1] != ' ') text += " ";
                    text += word;
                    if (cnt == line.Split(new [] {' '}).Length - 1)
                    {
                        if (res.Length > 0 && text.Length > 0)
                            res += "\r\n";
                        res += text;
                        text = "";
                    }
                }
                else
                {
                    string remain = word;
                    string[] parts = SplitWord(word, true);
                    if (parts != null) remain = "";
                    while (true)
                    {
                        if (parts == null) break;
                        remain = parts[1] + remain;
                        if (text.Length + parts[0].Length <= maxLength)
                        {
                            text += parts[0];
                            break;
                        }
                        parts = SplitWord(parts[0].TrimEnd('-'), true);
                        if (parts == null)
                            remain = word;
                    }
                    if (res.Length > 0 && text.Length > 0) res += "\r\n";
                    //тут растягиваем строку пробелами
                    if (insertSpaces)
                    {
                        text = text.TrimEnd(new [] {' '});
                        int cntr = maxLength - text.Length;
                        while (cntr > 0)
                        {
                            int index = 0;
                            if (text.IndexOf("   ", index) == 0) index = 4;
                            while (text.IndexOf(" ", index) != -1 && cntr > 0)
                            {
                                index = text.IndexOf(" ", index);
                                while (text[index] == ' ')
                                    index++;
                                text = text.Insert(index - 1, " ");
                                index++;
                                cntr--;
                            }
                        }
                    }
                    res += text;
                    text = remain;
                }
                cnt++;
            }
            if (res.Length > 0 && text.Length > 0)
                res += "\r\n";
            res += text;
            return res;
        }

        /// <summary>
        /// Если слово не влезает в отведённую для неё область, то вызывается эта функция,
        /// которая проверяет, а вдруг удастся перенести слово так, чтобы хотя бы часть
        /// его влезла в указанное пространство. Параметр EmptyAllowed равен False только
        /// в том случае, если слово - первое в строке. В этом случае, во-первых, не надо
        /// добавлять пробел перед словом, а во-вторых, если места слишком мало даже для
        /// минимально допустимой части слова, этоу часть всё равно надо оставить в строке,
        /// иначе - "У попа была собака".
        /// </summary>
        /// <returns></returns>
        private string[] SplitWord(string word, bool emptyAllowed)
        {
            string hp = Hyphen(word);
            string pref = emptyAllowed ? " " : "";
            for (int i = hp.Length - 1; i >= 0; i--)
            {
                switch (hp[i])
                {
                    case 'b':
                        if (pref.Length + i + 2 <= maxLength) //1 для переноса и 1 - поправка индекса i
                            return new[] {pref + word.Substring(0, i) + "-", word.Substring(i)};
                        break;
                    case 'c':
                        if (pref.Length + i + 1 <= maxLength) //Без учета "-"
                            return new[] {pref + word.Substring(0, i), word.Substring(i)};
                        break;
                }
            }
            if (!emptyAllowed)
            {
                for (int i = 0; i < hp.Length; i++)
                {
                    switch (hp[i])
                    {
                        case 'b':
                            return new[] {word.Substring(0, i) + "-", word.Substring(i)};
                        case 'c':
                            return new[] {word.Substring(0, i), word.Substring(i)};
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// А эта функция занимается окончательным выяснением, где можно поставить перенос.
        ///Слово - это то, что "от пробела до пробела", то есть оно может содержать и
        ///те знаки препинания, которые от слов пробелами не отделяются, и анлийские
        ///символы, и ещё что-нибудь. Эта функция выделяет из слов последовательности
        ///русских букв и отправляет их на анализ в предыдущую функцию RusHyps, а сама
        ///разбирается с остальными символами. Без особых размышлений она запрещает
        ///перенос на всех инородных символах, кроме дефиса. Если в слове встретился
        ///дефис, то в этом месте в результирующую строку вставляется "с". Например,
        ///Hyphen('что-нибудь')='aaacabaaa'.
        /// </summary>
        /// <returns></returns>
        private string Hyphen(string word)
        {
            string res = "";
            string rWd = "";
            foreach (Char ch in word)
            {
                if (ch == '-')
                {
                    if (rWd.Length > 0)
                    {
                        res += RusHyps(rWd);
                        rWd = "";
                    }
                    res += "c";
                }
                else if (RusLetters.Contains(ch.ToString()))
                    rWd += ch.ToString();
                else
                {
                    if (rWd.Length > 0)
                    {
                        res += RusHyps(rWd);
                        rWd = "";
                    }
                    res += "a";
                }
            }
            if (rWd.Length > 0)
                res += RusHyps(rWd);
            return res;
        }

        /// <summary>
        /// Эта функция смотрит, как можно перенести слово Wd, переданное в качестве параметра.
        ///Предполагается, что Wd не содержит никаких иных символов, кроме русских букв.
        ///Результатом работы является строка из символов a и b. Если стоит a - перенос
        ///в этом месте невозможен, если b - после этого места можно поставить перенос.
        ///Примеры:
        ///Исходное слово      Королевство   Дельфи
        ///Результат           ababaabbbaa   aaabaa
        ///Кому-то может показаться спорной возможность перенести слово в тех местах, которые
        ///указывает программа. Что ж, мой анализатор действительно несовершенен. Кто может -
        ///пусть напишет лучше.   
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private string RusHyps(string word)
        {
            string vowel = "аеёиоуыэюяАЕЁИОУЫЭЮЯ";
            string symbs = "ьЬъЪ";
            string cons = "БВГДЖЗЙКЛМНПРСТФХЦЧШЩбвгджзйклмнпрстфхцчшщ";

            string res = "";
            if (allowHyp)
            {
                int i = 0;
                bool vw = false;
                while (i < word.Length)
                {
                    if (vowel.Contains(word[i].ToString()) && res.Length > 0)
                    {
                        res = res.Remove(res.Length - 1) + "b";
                        vw = true;
                    }
                    while (i < word.Length && cons.Contains(word[i /* + 1*/].ToString()))
                    {
                        res += "a";
                        i++;
                    }
                    if (i == word.Length - 1)
                        break;
                    if (vowel.Contains(word[i /*+ 1*/].ToString()))
                    {
                        if (vw && res.Length > 1)
                            res = res.Remove(res.Length - 1) + "b";
                        else
                            vw = true;
                        res += "aa";
                        i += 2;
                    }
                    else if (symbs.Contains(word[i /* + 1*/].ToString()))
                    {
                        if (i + 1 == word.Length - 1)
                        {
                            res += "aa";
                            i++;
                            //--------подозрительно что увеличивается только на 1 а в остальных случаях на количаство "аа"----------------
                        }
                        else if (vowel.Contains(word[i + 1 /* + 2*/].ToString()))
                        {
                            if (vw && res.Length > 1)
                                res = res.Remove(res.Length - 1) + "b";
                            else
                                vw = true;
                            res += "aaa";
                            i += 3;
                        }
                        else
                        {
                            res += "aa";
                            i += 2;
                        }
                    }
                    else
                    {
                        res += "a";
                        i++;
                    }
                }
                if (word.Length != res.Length)
                    res += "a";
            }
            else
            {
                res = "";
                for (int i = 0; i < word.Length; i++)
                    res += "a";
            }
            return res;
        }
    }
}