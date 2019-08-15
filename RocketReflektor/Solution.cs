namespace RocketReflektor
{
    public class Solution : ISolution
    {
        private readonly IRocketReflektor _injectedDte;

        public Solution(IRocketReflektor injectedDte)
        {
            _injectedDte = injectedDte;
        }

        public IProjects Projects => new Projects();
    }
}
