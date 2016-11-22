namespace CFE
{
    struct StatBlock
    {
        public int str, agi, intelligence, armor;

        public StatBlock(int s, int a, int i, int armor)
        {
            str = s;
            agi = a;
            intelligence = i;
            this.armor = armor;
        }
    }
}
