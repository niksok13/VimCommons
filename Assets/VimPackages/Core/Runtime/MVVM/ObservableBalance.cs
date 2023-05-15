namespace VimPackages.Core.Runtime.MVVM
{
    public class ObservableBalance: ObservableData<int>
    {
        public ObservableBalance(string type) => this.ConnectPref(type);

        public bool CanPay(int amount)
        {
            if (amount < 0) return false;
            return Value >= amount;
        }

        public bool TryPay(int amount)
        {
            if (!CanPay(amount)) return false;
            Value -= amount;
            return true;
        }

        public void Receive(int amount)
        {
            if (amount < 0) return;
            Value += amount;
        }
    }
}