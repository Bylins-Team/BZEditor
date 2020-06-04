using System;

namespace DataUtils
{
    public class IngredientsCollection : BaseDataArrayList
    {
        public Ingredient Get(string typeName)
        {
            foreach (Ingredient ingredient in this)
            {
                if (ingredient.TypeName == typeName)
                    return ingredient;
            }
            return null;
        }

        public void Add(string typeName, int power, int percent)
        {
            if (Get(typeName) != null) return;
            var mi = new Ingredient(typeName, power, percent);
            Add(mi);
        }

        public void Add(string typeName, int percent)
        {
            if (Get(typeName) != null) return;
            var mi = new Ingredient(typeName, percent);
            Add(mi);
        }

        public void Update(string typeName, int power, int percent)
        {
            Ingredient ingredient = Get(typeName);
            if (ingredient != null)
                ingredient.Probability = percent;
        }

        public void Remove(string typeName)
        {
            Ingredient ingredient = Get(typeName);
            if (ingredient != null)
                Remove(ingredient);
        }

        public void ReplacePower(Guid internalGuid, int newVal)
        {
            foreach (Ingredient i in this)
            {
                if (i.InternaGuid == internalGuid && i.Power != newVal)
                {
                    i.Power = newVal;
                    i.PowerAuto = false;
                    FireChangeEvent(this);
                    return;
                }
            }
        }

        public void ReplaceProbability(Guid internalGuid, int newVal)
        {
            foreach (Ingredient i in this)
            {
                if (i.InternaGuid == internalGuid && i.Probability != newVal)
                {
                    i.Probability = newVal;
                    FireChangeEvent(this);
                    return;
                }
            }
        }

        public void ReplacePowerAuto(Guid internalGuid, bool newVal)
        {
            foreach (Ingredient i in this)
            {
                if (i.InternaGuid == internalGuid && i.PowerAuto != newVal)
                {
                    i.PowerAuto = newVal;
                    if (newVal)
                        i.Power = 0;
                    FireChangeEvent(this);
                    return;
                }
            }
        }

        public new IngredientsCollection Clone()
        {
            var res = new IngredientsCollection();
            foreach (Ingredient ingredient in this)
                res.Add(ingredient.TypeName, ingredient.Power, ingredient.Probability);
            return res;
        }
    }
}