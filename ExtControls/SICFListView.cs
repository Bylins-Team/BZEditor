/*При изменении количества выбранных строк событие OnSelectedIndexChanged по умолчанию
 * происходит сначала для отмены каждой из выбранных строк, а затем для выбора заново
 * строк, кторые должны быть выбраны в новом состоянии SelectedItems.
 * Оба этих процесса происходят от меньшего индекса к большему!.
 * Причем это только с Shift
 * С Ctrl такого не происходит
 */
using System;
using System.Windows.Forms;

namespace ExtControls
{
    public class SICFListView : ListView
    {
        private int Curr = -1;
        private bool KeyPressed;
        private int Prev = -1;
        private int PrevSelectedCount;
        private bool Shift;

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            if (!MultiSelect && View == View.Details) //Работает только для мультиселекта в Details
            {
                base.OnSelectedIndexChanged(e);
                return;
            }
            if (SelectedItems.Count == 0) return;
            if (KeyPressed)
            {
                KeyPressed = false;
                base.OnSelectedIndexChanged(e);
                return;
            }
            if (!Shift)
                if (PrevSelectedCount > 0 && SelectedIndices[SelectedIndices.Count - 1] != Curr)
                {
                    //Добавил вызов базового метода а то с контролом вообще не работал толком селект :)
                    base.OnSelectedIndexChanged(e);
                    return;
                }

            if (Prev + PrevSelectedCount <= Curr) //возможно надо <=
            {
                //Случай когда выбрано c шифтом с 8 по 3  и не отпуская шифта кликается 10
                if (Shift && PrevSelectedCount > 0 && SelectedIndices[SelectedIndices.Count - 1] == Curr)
                {
                    base.OnSelectedIndexChanged(e);
                    return;
                }
            }
            else if (Prev - PrevSelectedCount >= Curr && SelectedIndices.Count > 1) //возможно надо >=
            {
                //Случай когда выбрано c шифтом с 3 по 8 и не отпуская шифта кликается 1 или 2
                if (Shift && PrevSelectedCount > 0 &&
                    SelectedIndices[SelectedIndices.Count - 2] == Prev - 1 - PrevSelectedCount)
                {
                    base.OnSelectedIndexChanged(e);
                    return;
                }
            }
            if (SelectedIndices.Count > PrevSelectedCount)
            {
                //выбрано с 5 по 10, с шифтом кликаем 15
                if (Shift && PrevSelectedCount > 0 && Prev < Curr && SelectedIndices[SelectedIndices.Count - 1] != Curr)
                    return;
                //Выбран 10 затем с шифтом кликаем 5. при этом последняя выбранная клетка всегда остается с индексом Prev проверяем по строке с индексом на 1 меньше
                if (Shift && PrevSelectedCount > 0 && Prev > Curr &&
                    SelectedIndices[SelectedIndices.Count - 1 - PrevSelectedCount] != Prev - 1) return;
            }
            if (SelectedIndices.Count < PrevSelectedCount)
            {
                //выбрано с 10 по 3, с шифтом кликаем 5
                if (Shift && PrevSelectedCount > 0 && Prev < Curr && SelectedIndices[0] != Curr) return;
                //Выбрано с 5 по 10 кликаем 7
                if (Shift && PrevSelectedCount > 0 && Prev > Curr && SelectedIndices[SelectedIndices.Count - 1] != Curr)
                    return;
            }
            if (Prev == Curr && SelectedIndices[0] != Curr) return;
            base.OnSelectedIndexChanged(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (GetItemAt(e.X, e.Y) == null) return;
            Prev = Curr;
            Curr = GetItemAt(e.X, e.Y).Index;
            base.OnMouseDown(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            PrevSelectedCount = SelectedIndices.Count;
            Shift = e.Shift;
            //Это чтоб кнопки заработали :)
            KeyPressed = ((e.KeyData & Keys.KeyCode) == Keys.Up || (e.KeyData & Keys.KeyCode) == Keys.Down ||
                          (e.KeyData & Keys.KeyCode) == Keys.PageUp || (e.KeyData & Keys.KeyCode) == Keys.PageDown ||
                          (e.KeyData & Keys.KeyCode) == Keys.Home || (e.KeyData & Keys.KeyCode) == Keys.End);
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            Shift = e.Shift;
            base.OnKeyUp(e);
        }
    }
}