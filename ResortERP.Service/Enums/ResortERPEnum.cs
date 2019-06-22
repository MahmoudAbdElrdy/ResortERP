namespace ResortERP.Service
{


    public enum AccountEnum
    {

       
        //عادي
        Acc0 = 1,
        //ختامي
        Acc1 = 2,
        //تجميعى
        Acc2 = 3,
        //توزيعى
        Acc3 = 4,
        //عادي عميل
        Acc4 = 5,
        //عادي مورد
        Acc5 = 6,
        //عادي عميل ومورد
        Acc6 = 7,
        //عادي صندوق نقدي
        Acc7 = 8,
        //عادي بنك
        Acc8 = 9,
        //عادي صندوق وبنك
        Acc9 = 10,
        //عادي صندوق الذهب
        Acc10 = 13,
        //عورشة
        Acc11 = 21
    }


    public enum AccountCustomersEnum
    {
        //عميل
        Cust_Acc0 = 5,
        //مورد
        Cust_Acc1 = 6,
        //عميل ومورد
        Cust_Acc2 = 7
    }

    public enum AccountWorkshopsEnum
    {
        //ورشة
        Workshop0 = 21
      
    }
    public enum AccountBoxEnum
    {
        //عادي صندوق
        Box_Acc0 = 8,
        //عادي صندوق وبنك
        Box_Acc1 = 10
    }

    public enum AccountGoldBoxEnum
    {
        //عادي صندوق الذهب
        GoldBox_Acc0 = 13,
       
    }

    public enum EmployessTypesEnum
    {
        Sales = 1
    }

    public enum SellTypes
    {
        Whole = 1,
        Half_Whole = 2,
        Retail = 3,
        EndUsers = 4,
        Employee = 5,
        Export = 6,
        Last_Buy = 7

    }
}
