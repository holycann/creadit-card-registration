using System.ComponentModel;

namespace CC_Regist_System.Enums
{
    public enum CardNameEnum
    {
        [Description("ABC BANK Syariah Standard")]
        AbcBankSyariahStandard,

        [Description("ABC BANK Gold")]
        AbcBankGold,

        [Description("ABC BANK Expense Platinum")]
        AbcBankExpensePlatinum,
    }

    public enum CardTypeEnum
    {
        Visa,
        Mastercard,
    }

    public enum CardTierEnum
    {
        Standard,
        Gold,
        Platinum,
    }
}
