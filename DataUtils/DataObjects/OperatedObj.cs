namespace DataUtils
{
    public class OperatedObj : BaseDataObject
    {
        public readonly OperatedObjCollection ObjectsInObject = new OperatedObjCollection();
        private int probability = 100;

        /// <summary>
        /// Mob/object-type TRIGGER and VAR reset commands that followed this spawn, as .zon letter lines.
        /// The engine binds them to the entity loaded just before (tmob/tobj), so they are written right
        /// after this spawn's own line. There is no UI for them; they are carried through unchanged.
        /// </summary>
        public readonly System.Collections.Generic.List<string> AnchoredCommands = new System.Collections.Generic.List<string>();

        private int loadType;
        public OperatedObj(int inVNum)
        {
            VNum = inVNum;
            ObjectsInObject.Changed += FireChangeEvent;
        }

        /// <summary>
        /// Вероятность загрузки
        /// </summary>
        public int Probability
        {
            get => probability;
            set
            {
                if (probability == value) return;
                if (value == -1) value = 100;
                probability = value;
                FireChangeEvent(this);
            }
        }

        /// <summary>
        /// 0 - загружать всегда.
        /// 1 - загружать если предыдущий предмет списка был загружен.
        /// 2 - загружать всегда, не менять результата предыдущей загрузки.
        /// 3 - загружать если был загружен предыдущий, не менять результата.
        /// </summary>
        public int LoadType
        {
            get => loadType;
            set
            {
                if (loadType == value) return;
                loadType = value;
                FireChangeEvent(this);
            }
        }
    }
}