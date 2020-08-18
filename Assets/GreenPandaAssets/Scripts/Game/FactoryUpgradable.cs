namespace GreenPandaAssets.Scripts
{
    public class FactoryUpgradable : AUpgradable
    {
        public FactoryView FactoryView;

        private void Update()
        {
        
        }
        
        public override void Upgrade()
        {
            base.Upgrade();

            var skinLevel = -1;
            
            if (_level <= 5)
            {
                skinLevel = 1;
            }
            else if (_level <= 10)
            {
                skinLevel = 2;
            }
            else
            {
                skinLevel = 3;
            }
            
            FactoryView.SetSkinLevel(skinLevel);
        }
    }
}