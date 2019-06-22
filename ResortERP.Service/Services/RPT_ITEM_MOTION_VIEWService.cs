using ResortERP.Core;
using ResortERP.Core.VM;
using ResortERP.Repository;
using ResortERP.Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortERP.Service.Services
{
    public class RPT_ITEM_MOTION_VIEWService : IRPT_ITEM_MOTION_VIEWService, IDisposable
    {
        IGenericRepository<RPT_ITEM_MOTION_VIEW> itemMotionRepo;

        public RPT_ITEM_MOTION_VIEWService(IGenericRepository<RPT_ITEM_MOTION_VIEW> itemMotionRepo)
        {
            this.itemMotionRepo = itemMotionRepo;
        }



        public void Dispose()
        {
            itemMotionRepo.Dispose();
        }

        public List<RPT_ITEM_MOTION_VIEW_VM> GetAll()
        {
            var q = from entity in itemMotionRepo.GetAll()
                    select new RPT_ITEM_MOTION_VIEW_VM
                    {
                        ACC_AR_NAME = entity.ACC_AR_NAME,
                        ACC_CODE = entity.ACC_CODE,
                        ACC_EN_NAME = entity.ACC_EN_NAME,
                        BILL_ABRV_AR = entity.BILL_ABRV_AR,
                        BILL_ABRV_EN = entity.BILL_ABRV_EN,
                        BILL_AR_NAME = entity.BILL_AR_NAME,
                        BILL_DATE = entity.BILL_DATE,
                        BILL_EN_NAME = entity.BILL_EN_NAME,
                        BILL_ID = entity.BILL_ID,
                        BILL_IS_POST = entity.BILL_IS_POST,
                        BILL_NUMBER = entity.BILL_NUMBER,
                        BILL_PAY_WAY = entity.BILL_PAY_WAY,
                        BILL_REMARKS = entity.BILL_REMARKS,
                        BILL_SETTING_ID = entity.BILL_SETTING_ID,
                        BILL_TOTAL = entity.BILL_TOTAL,
                        BILL_TOTAL_DISC = entity.BILL_TOTAL_DISC,
                        BILL_TOTAL_EXTRA = entity.BILL_TOTAL_EXTRA,
                        BILL_TYPE = entity.BILL_TYPE,
                        COM_STORE_AR_NAME = entity.COM_STORE_AR_NAME,
                        COM_STORE_CODE = entity.COM_STORE_CODE,
                        COM_STORE_EN_NAME = entity.COM_STORE_EN_NAME,
                        COM_STORE_ID = entity.COM_STORE_ID,
                        COST_CENTER_ID = entity.COST_CENTER_ID,
                        EMP_ID = entity.EMP_ID,
                        GRID_ROW_NUMBER = entity.GRID_ROW_NUMBER,
                        TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                        UNIT_ID = entity.UNIT_ID,
                        CUST_ACC_ID = entity.CUST_ACC_ID,
                        DISC_RATE = entity.DISC_RATE,
                        DISC_VALUE = entity.DISC_VALUE,
                        EMP_AR_NAME = entity.EMP_AR_NAME,
                        EMP_CODE = entity.EMP_CODE,
                        EMP_EN_NAME = entity.EMP_EN_NAME,
                        EXTRA_RATE = entity.EXTRA_RATE,
                        EXTRA_VALUE = entity.EXTRA_VALUE,
                        GIFT = entity.GIFT,
                        GROUP_ID = entity.GROUP_ID,
                        ITEM_AR_NAME = entity.ITEM_AR_NAME,
                        ITEM_CODE = entity.ITEM_CODE,
                        ITEM_EN_NAME = entity.ITEM_EN_NAME,
                        ITEM_ID = entity.ITEM_ID,
                        QTY = entity.QTY,
                        UNIT_AR_NAME = entity.UNIT_AR_NAME,
                        UNIT_CODE = entity.UNIT_CODE,
                        UNIT_EN_NAME = entity.UNIT_EN_NAME,
                        UNIT_PRICE = entity.UNIT_PRICE,
                        UNIT_TRANS_RATE = entity.UNIT_TRANS_RATE
                    };
            return q.ToList();
        }

        public Task<List<RPT_ITEM_MOTION_VIEW_VM>> GetAllAsync(int pageNum, int pageSize)
        {
            return Task.Run(() =>
            {
                int rowCount;
                var q = from entity in itemMotionRepo.GetPaged<long?>(pageNum, pageSize, p => p.BILL_ID, false, out rowCount)
                        select new RPT_ITEM_MOTION_VIEW_VM
                        {
                            ACC_AR_NAME = entity.ACC_AR_NAME,
                            ACC_CODE = entity.ACC_CODE,
                            ACC_EN_NAME = entity.ACC_EN_NAME,
                            BILL_ABRV_AR = entity.BILL_ABRV_AR,
                            BILL_ABRV_EN = entity.BILL_ABRV_EN,
                            BILL_AR_NAME = entity.BILL_AR_NAME,
                            BILL_DATE = entity.BILL_DATE,
                            BILL_EN_NAME = entity.BILL_EN_NAME,
                            BILL_ID = entity.BILL_ID,
                            BILL_IS_POST = entity.BILL_IS_POST,
                            BILL_NUMBER = entity.BILL_NUMBER,
                            BILL_PAY_WAY = entity.BILL_PAY_WAY,
                            BILL_REMARKS = entity.BILL_REMARKS,
                            BILL_SETTING_ID = entity.BILL_SETTING_ID,
                            BILL_TOTAL = entity.BILL_TOTAL,
                            BILL_TOTAL_DISC = entity.BILL_TOTAL_DISC,
                            BILL_TOTAL_EXTRA = entity.BILL_TOTAL_EXTRA,
                            BILL_TYPE = entity.BILL_TYPE,
                            COM_STORE_AR_NAME = entity.COM_STORE_AR_NAME,
                            COM_STORE_CODE = entity.COM_STORE_CODE,
                            COM_STORE_EN_NAME = entity.COM_STORE_EN_NAME,
                            COM_STORE_ID = entity.COM_STORE_ID,
                            COST_CENTER_ID = entity.COST_CENTER_ID,
                            EMP_ID = entity.EMP_ID,
                            GRID_ROW_NUMBER = entity.GRID_ROW_NUMBER,
                            TO_COM_STORE_ID = entity.TO_COM_STORE_ID,
                            UNIT_ID = entity.UNIT_ID,
                            CUST_ACC_ID = entity.CUST_ACC_ID,
                            DISC_RATE = entity.DISC_RATE,
                            DISC_VALUE = entity.DISC_VALUE,
                            EMP_AR_NAME = entity.EMP_AR_NAME,
                            EMP_CODE = entity.EMP_CODE,
                            EMP_EN_NAME = entity.EMP_EN_NAME,
                            EXTRA_RATE = entity.EXTRA_RATE,
                            EXTRA_VALUE = entity.EXTRA_VALUE,
                            GIFT = entity.GIFT,
                            GROUP_ID = entity.GROUP_ID,
                            ITEM_AR_NAME = entity.ITEM_AR_NAME,
                            ITEM_CODE = entity.ITEM_CODE,
                            ITEM_EN_NAME = entity.ITEM_EN_NAME,
                            ITEM_ID = entity.ITEM_ID,
                            QTY = entity.QTY,
                            UNIT_AR_NAME = entity.UNIT_AR_NAME,
                            UNIT_CODE = entity.UNIT_CODE,
                            UNIT_EN_NAME = entity.UNIT_EN_NAME,
                            UNIT_PRICE = entity.UNIT_PRICE,
                            UNIT_TRANS_RATE = entity.UNIT_TRANS_RATE
                        };
                return q.ToList();
            });
        }

        public List<RPT_ITEM_MOTION_VIEW_VM> GetingSearchItems(SearchingmainItems smItem, List<BILL_SETTINGSVM> bilSettings, List<ReportOptions> rptOptions)
        {
            string WhereCondition = "";

            WhereCondition += "COM_BRN_ID in(" + smItem.companyBranches + ") AND ";
            WhereCondition += smItem.CostCenterID == null ? "" : "COST_CENTER_ID = " + smItem.CostCenterID + " AND ";
            //WhereCondition += smItem.Currency != null ? "" :  "CURRENCY_ID = " + smItem.Currency + " AND ";
            WhereCondition += smItem.EmployeeID == null ? "" : "EMP_ID = " + smItem.EmployeeID + " AND ";
            WhereCondition += smItem.EndDate == null ? "" : "BILL_DATE <= " + smItem.EndDate + " AND ";
            WhereCondition += smItem.StartDate == null ? "" : "BILL_DATE >= " + smItem.StartDate + " AND ";
            WhereCondition += smItem.itemID == null ? "" : "ITEM_ID = " + smItem.itemID + " AND ";
            WhereCondition += smItem.itemUnitID == null ? "" : "UNIT_ID = " + smItem.itemUnitID + " AND ";
            WhereCondition += smItem.storeID == null ? "" : "(COM_STORE_ID = " + smItem.storeID + " OR TO_COM_STORE_ID = " + smItem.storeID + ") and ";

            if (rptOptions != null)
            {
                WhereCondition += "(";
                WhereCondition += rptOptions.Any(x => x.ID == "1") ? "BILL_IS_POST = 1 OR " : "";
                WhereCondition += rptOptions.Any(x => x.ID == "2") ? "BILL_IS_POST = 0 OR " : "";
                if (WhereCondition.Contains("BILL_IS_POST"))
                    WhereCondition = WhereCondition.Remove(WhereCondition.Length - 4, 3) + ") AND ";
                else
                    WhereCondition = WhereCondition.Remove(WhereCondition.Length - 1, 1);
            }

            if (bilSettings != null)
            {
                WhereCondition += "(";
                for (int i = 0; i < bilSettings.Count; i++)
                {
                    WhereCondition += "BILL_SETTING_ID = " + bilSettings[i].BILL_SETTING_ID + " OR ";
                }
                WhereCondition = WhereCondition.Remove(WhereCondition.Length - 4, 3) + ") AND ";
            }

            WhereCondition += smItem.finalize == null ? "1=1" : "BILL_PAY_WAY = " + smItem.finalize;



            var ItemMotionList = itemMotionRepo.SQLQuery<RPT_ITEM_MOTION_VIEW_VM>("select * from RPT_ITEM_MOTION_VIEW Where " + WhereCondition).ToList();
            return ItemMotionList;

            throw new NotImplementedException();
        }

        //public bool Preview()
        //{

        //double total = 0;
        //double qty = 0;
        //double mid = 0;
        //double last_qty = 0;
        //double tb = 0;
        //if (TBItem.Text.Trim() == "")
        //{
        //    mdimain.show_message_box(1, "يجب تحديد الصنف", "خطأ");
        //    return false;
        //}

        //#region **check date**
        //if (system.check_StartDate(DTPStartDate1.Value))
        //{
        //}
        //else
        //{
        //    mdimain.show_message_box(MainParent.OK, "تاريخ البداية قبل بداية المدة", "خطأ");
        //    return false;
        //}
        //if (system.check_EndDate(DTPStartDate1.Value))
        //{
        //}
        //else
        //{
        //    mdimain.show_message_box(MainParent.OK, "تاريخ البداية  بعد نهاية المدة", "خطأ");
        //    return false;
        //}

        ////------------------------------
        //if (system.check_StartDate(DTPFinishDate.Value))
        //{
        //}
        //else
        //{
        //    mdimain.show_message_box(MainParent.OK, "تاريخ النهاية قبل بداية المدة", "خطأ");
        //    return false;
        //}
        //if (system.check_EndDate(DTPFinishDate.Value))
        //{
        //}
        //else
        //{
        //    mdimain.show_message_box(MainParent.OK, "تاريخ النهاية بعد نهاية المدة", "خطأ");
        //    return false;
        //}

        ////-----------------------------------------------------------------------
        //#endregion

        //item_motion = new ITEMS_MOTION_PREVIEW(mdimain);
        //item_motion.item = this;
        //item_motion.DGVRPT.Columns.Clear();
        //item_motion.dataGridView1.Columns.Clear();
        //setGrid();
        //double CIBillQty = 0;
        //double CIBillTotal = 0;
        //double COBillQty = 0;
        //double COBillTotal = 0;

        //double CTBillQty = 0;
        //double CTBillTotal = 0;

        //item_motion.Bill_type.Clear();
        //item_motion.Bill_text.Clear();
        //item_motion.Bill_id.Clear();
        ////------------------------------
        //filter = "";
        //options = "(";
        //where = "";
        //post = " and (";
        ////------------------------------مصادر التقارير
        //for (int j = 0; j < CLBReportResources.Items.Count; j++)
        //{
        //    if (Convert.ToBoolean(CLBReportResources.GetItemCheckState(j)) == true)
        //    {
        //        options = options + " BILL_SETTING_ID = " + CLBReportResources.IDItems[j].ToString() + " or ";
        //    }
        //}

        //if (options == "(")
        //{
        //    mdimain.show_message_box(MainParent.OK, "يجب أختيار أحد مصادر التقارير", "خطأ");
        //    return false;
        //}
        ////---------------------------------------------------------
        //options = options.Substring(0, options.Length - 4);
        //options = options + ")";
        ////----------------------------
        //if (TBItem.Text.Trim() != "")
        //{

        //    where = where + " and ITEM_ID = " + Convert.ToString(item_id);


        //}
        ////--------------------------------
        //if (TBCompStore.Text.Trim() != "")
        //{
        //    where = where + " and ( COM_STORE_ID = " + Convert.ToString(Com_Store_ID) + " OR TO_COM_STORE_ID = " + Convert.ToString(Com_Store_ID) + " )";
        //}
        ////----------------------------------------------------
        //if (TBCostCenter.Text.Trim() != "")
        //{
        //    where = where + " and COST_CENTER_ID = " + Convert.ToString(Cost_Center_ID);
        //}
        ////-------------------------------------------------------
        //if (TBEmployee.Text.Trim() != "")
        //{
        //    where = where + " and EMP_ID = " + Convert.ToString(Emp_ID);
        //}
        ////--------------------------مرحلة وغير المرحلة
        //if (CHBPostedView.Checked)
        //{
        //    post = post + " BILL_IS_POST = 1 or ";
        //}
        //if (CHBNotPostedView.Checked)
        //{
        //    post = post + " BILL_IS_POST = 0 or ";
        //}
        //post.Substring(0, 4);
        //post = post + ")";
        ////----------------------------تصفية نقدى او اجل
        //if (CBOptions.SelectedIndex == 1)
        //    filter = " and BILL_PAY_WAY = 1 ";
        //else if (CBOptions.SelectedIndex == 2)
        //    filter = " and BILL_PAY_WAY = 2 ";
        ////-----------------------------
        //try
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    if (CHBNotPostedView.Checked == true)
        //    {
        //        cmd = new SqlCommand("select * from RPT_ITEM_MOTION_VIEW " + " WHERE ITEM_ID = " + Convert.ToString(item_id) + " and BILL_DATE >='" + DTPStartDate1.Value.ToString("yyyy/MM/dd") + "' and BILL_DATE <=CONVERT(DATETIME,'" + DTPFinishDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "', 102)", MainParent.dbcon);

        //    }
        //    else
        //    {
        //        cmd = new SqlCommand("select * from RPT_ITEM_MOTION_VIEW " + " WHERE ITEM_ID = " + Convert.ToString(item_id) + " and BILL_DATE >='" + DTPStartDate1.Value.ToString("yyyy/MM/dd") + "' and BILL_DATE <=CONVERT(DATETIME,'" + DTPFinishDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + "', 102) AND BILL_IS_POST=1", MainParent.dbcon);

        //    }

        //    cmd.CommandType = CommandType.Text;
        //    data = new SqlDataAdapter(cmd);
        //    DT_Report.Reset();
        //    cmd.CommandTimeout = 3000; data.Fill(DT_Report);

        //    //------------------------Purchase info----------------------------------------
        //    cmd = new SqlCommand("select min(UNIT_PRICE) , max(UNIT_PRICE) , avg(UNIT_PRICE) from RPT_ITEM_MOTION_VIEW where (BILL_TYPE= 1 or BILL_TYPE= 9) and BILL_DATE >='" + DTPStartDate1.Value.ToString("yyyy/MM/dd") + "' and BILL_DATE <='" + DTPFinishDate.Value.ToString("yyyy/MM/dd") + "'" + filter + where + " AND UNIT_ID= " + _Unit_ID, MainParent.dbcon);
        //    cmd.CommandType = CommandType.Text;
        //    data = new SqlDataAdapter(cmd);
        //    DT_Info_Buy.Reset();
        //    cmd.CommandTimeout = 3000;
        //    data.Fill(DT_Info_Buy);
        //    //------------------------sell info--------------------------------------------
        //    cmd = new SqlCommand("select min(UNIT_PRICE) , max(UNIT_PRICE) , avg(UNIT_PRICE) from RPT_ITEM_MOTION_VIEW where (BILL_TYPE= 2) and BILL_DATE >='" + DTPStartDate1.Value.ToString("yyyy/MM/dd") + "' and BILL_DATE <='" + DTPFinishDate.Value.ToString("yyyy/MM/dd") + "'" + filter + where + " AND UNIT_ID= " + _Unit_ID, MainParent.dbcon);
        //    cmd.CommandType = CommandType.Text;
        //    data = new SqlDataAdapter(cmd);
        //    DT_Info_Sell.Reset();
        //    cmd.CommandTimeout = 3000;
        //    data.Fill(DT_Info_Sell);
        //    //-------------------------------------------------------------------------------

        //    DR = DT_Report.Select(" BILL_DATE >='" + DTPStartDate1.Value.ToString("yyyy/MM/dd") + "' and BILL_DATE <='" + DTPFinishDate.Value + "' and " + options + filter + where);

        //    DR_Last = DT_Report.Select(" BILL_DATE <'" + DTPStartDate1.Value.ToString("yyyy/MM/dd") + "' and " + options + filter + where);
        //}
        //catch
        //{
        //    mdimain.show_message_box(1, "هناك خطأ فى الإتصال", "خطأ");
        //    return false;
        //}
        //if (DR.Length < 1)
        //{
        //    mdimain.show_message_box(1, "لا توجد حركة على هذا الصنف", "خطأ");
        //    return false;
        //}

        //if (DR_Last.Length > 0)
        //{
        //    for (int i = 0; i <= DR_Last.Length - 1; i++)
        //    {
        //        if (Convert.ToInt32(Convert.ToInt32(DR_Last[i]["BILL_TYPE"].ToString()).ToString()) == 1 || Convert.ToInt32(DR_Last[i]["BILL_TYPE"].ToString()) == 4 || Convert.ToInt32(DR_Last[i]["BILL_TYPE"].ToString()) == 5 || Convert.ToInt32(DR_Last[i]["BILL_TYPE"].ToString()) == 9)
        //        {
        //            //--------------------------------كمية
        //            if (DR_Last[i]["QTY"] != null)
        //                if (DR_Last[i]["QTY"].ToString() != "")
        //                    if (DR_Last[i]["GIFT"] != null)
        //                        if (DR_Last[i]["GIFT"].ToString() != "")
        //                            last_qty = last_qty + (Convert.ToDouble(DR_Last[i]["QTY"].ToString()) + Convert.ToDouble(DR_Last[i]["GIFT"].ToString()));
        //                        else
        //                            last_qty = last_qty + Convert.ToDouble(DR_Last[i]["QTY"].ToString());
        //                    else
        //                        last_qty = last_qty + Convert.ToDouble(DR_Last[i]["QTY"].ToString());
        //        }
        //        else if (Convert.ToInt32(Convert.ToInt32(DR_Last[i]["BILL_TYPE"].ToString()).ToString()) == 2 || Convert.ToInt32(DR_Last[i]["BILL_TYPE"].ToString()) == 3 || Convert.ToInt32(DR_Last[i]["BILL_TYPE"].ToString()) == 6 || Convert.ToInt32(DR_Last[i]["BILL_TYPE"].ToString()) == 11)
        //        {
        //            //--------------------------------كمية
        //            if (DR_Last[i]["QTY"] != null)
        //                if (DR_Last[i]["QTY"].ToString() != "")
        //                    if (DR_Last[i]["GIFT"] != null)
        //                        if (DR_Last[i]["GIFT"].ToString() != "")
        //                            last_qty = last_qty - (Convert.ToDouble(DR_Last[i]["QTY"].ToString()) + Convert.ToDouble(DR_Last[i]["GIFT"].ToString()));
        //                        else
        //                            last_qty = last_qty - Convert.ToDouble(DR_Last[i]["QTY"].ToString());
        //                    else
        //                        last_qty = last_qty - Convert.ToDouble(DR_Last[i]["QTY"].ToString());
        //        }

        //    }
        //}
        ////------------------------------------
        //item_motion.LbLastQty.Text = system.set_QtyPoint(last_qty.ToString());
        ////-----------------------------headers
        //item_motion.LBItem.Text = TBItem.Text.Trim();
        //item_motion.LBCostCenter.Text = TBCostCenter.Text.Trim();
        //item_motion.LBStore.Text = TBCompStore.Text.Trim();
        //item_motion.LBEmp.Text = TBEmployee.Text.Trim();
        //item_motion.LBDate.Text = DTPStartDate1.Value.ToString("yyyy/MM/dd");
        //item_motion.LBToDate.Text = DTPFinishDate.Value.ToString("yyyy/MM/dd");
        ////--------------------------أسعار المبيع والشراء-----------------------------------------
        //if (DT_Info_Buy.Rows.Count > 0)
        //{
        //    if (DT_Info_Buy.Rows[0][1] != null)
        //        item_motion.LBMaxBuy.Text = system.set_PricePoint(DT_Info_Buy.Rows[0][1].ToString());
        //    if (DT_Info_Buy.Rows[0][0] != null)
        //        item_motion.LBMinBuy.Text = system.set_PricePoint(DT_Info_Buy.Rows[0][0].ToString());
        //    if (DT_Info_Buy.Rows[0][2] != null)
        //        item_motion.LBMidBuy.Text = system.set_PricePoint(DT_Info_Buy.Rows[0][2].ToString());
        //}

        //if (DT_Info_Sell.Rows.Count > 0)
        //{
        //    if (DT_Info_Sell.Rows[0][1] != null)
        //        item_motion.LBMaxSell.Text = system.set_PricePoint(DT_Info_Sell.Rows[0][1].ToString());
        //    if (DT_Info_Sell.Rows[0][0] != null)
        //        item_motion.LBMinSell.Text = system.set_PricePoint(DT_Info_Sell.Rows[0][0].ToString());
        //    if (DT_Info_Sell.Rows[0][2] != null)
        //        item_motion.LBMidSell.Text = system.set_PricePoint(DT_Info_Sell.Rows[0][2].ToString());
        //}
        ////--------------------------------------------------------------------
        //try
        //{
        //    for (int i = 0; i <= DR.Length - 1; i++)
        //    {
        //        item_motion.DGVRPT.Rows.Add(1);
        //        // item_motion.dataGridView1.Rows.Add(1);
        //        if (MainParent.lang == "A")
        //        {
        //            item_motion.DGVRPT.Rows[i].Cells["CBillNumber"].Value = DR[i]["BILL_ABRV_AR"].ToString() + ":" + DR[i]["BILL_NUMBER"].ToString();

        //            //-----------------------------------------الوحدة
        //            if (DR[i]["UNIT_CODE"] != null)
        //            {
        //                if (DR[i]["UNIT_CODE"].ToString() != "")
        //                {
        //                    item_motion.DGVRPT.Rows[i].Cells["CBillUnit"].Value = DR[i]["UNIT_CODE"].ToString().Trim() + "-" + DR[i]["UNIT_AR_NAME"].ToString().Trim();

        //                }
        //            }
        //            //-----------------------------------------المخزن
        //            if (CHBStoreView.Checked)
        //            {
        //                if (DR[i]["COM_STORE_CODE"] != null)
        //                {
        //                    if (DR[i]["COM_STORE_CODE"].ToString() != "")
        //                    {
        //                        item_motion.DGVRPT.Rows[i].Cells["CBillStore"].Value = DR[i]["COM_STORE_CODE"].ToString().Trim() + "-" + DR[i]["COM_STORE_AR_NAME"].ToString().Trim();

        //                    }
        //                }
        //            }
        //            //---------------------------------------البائع
        //            if (CHBEmployeeView.Checked)
        //            {
        //                if (DR[i]["EMP_CODE"] != null)
        //                {
        //                    if (DR[i]["EMP_CODE"].ToString() != "")
        //                    {
        //                        item_motion.DGVRPT.Rows[i].Cells["CBillEmployee"].Value = DR[i]["EMP_CODE"].ToString().Trim() + "-" + DR[i]["EMP_AR_NAME"].ToString().Trim();

        //                    }
        //                }
        //            }
        //            //----------------------bill text
        //            if (DR[i]["BILL_AR_NAME"] != null)
        //                item_motion.Bill_text.Add(DR[i]["BILL_AR_NAME"]);
        //            else
        //                item_motion.Bill_text.Add("");
        //        }
        //        else
        //        {
        //            item_motion.DGVRPT.Rows[i].Cells["CBillNumber"].Value = DR[i]["BILL_ABRV_EN"].ToString() + ":" + DR[i]["BILL_NUMBER"].ToString();

        //            //-----------------------------------------الوحدة
        //            if (DR[i]["UNIT_CODE"] != null)
        //            {
        //                if (DR[i]["UNIT_CODE"].ToString() != "")
        //                {
        //                    item_motion.DGVRPT.Rows[i].Cells["CBillUnit"].Value = DR[i]["UNIT_CODE"].ToString().Trim() + "-" + DR[i]["UNIT_EN_NAME"].ToString().Trim();

        //                }
        //            }
        //            //-----------------------------------------المخزن
        //            if (CHBStoreView.Checked)
        //            {
        //                if (DR[i]["COM_STORE_CODE"] != null)
        //                {
        //                    if (DR[i]["COM_STORE_CODE"].ToString() != "")
        //                    {
        //                        item_motion.DGVRPT.Rows[i].Cells["CBillStore"].Value = DR[i]["COM_STORE_CODE"].ToString().Trim() + "-" + DR[i]["COM_STORE_EN_NAME"].ToString().Trim();

        //                    }
        //                }
        //            }
        //            //----------------------------------------البائع
        //            if (CHBEmployeeView.Checked)
        //            {
        //                if (DR[i]["EMP_CODE"] != null)
        //                {
        //                    if (DR[i]["EMP_CODE"].ToString() != "")
        //                    {
        //                        item_motion.DGVRPT.Rows[i].Cells["CBillEmployee"].Value = DR[i]["EMP_CODE"].ToString().Trim() + "-" + DR[i]["EMP_EN_NAME"].ToString().Trim();

        //                    }
        //                }
        //            }
        //            //----------------------bill text
        //            if (DR[i]["BILL_EN_NAME"] != null)
        //                item_motion.Bill_text.Add(DR[i]["BILL_EN_NAME"]);
        //            else
        //                item_motion.Bill_text.Add("");
        //        }
        //        if (DR[i]["BILL_DATE"] != null)
        //        {
        //            item_motion.DGVRPT.Rows[i].Cells["CBillDate"].Value = Convert.ToDateTime(DR[i]["BILL_DATE"].ToString()).ToString("yyyy/MM/dd");

        //        }
        //        else
        //            item_motion.DGVRPT.Rows[i].Cells["CBillDate"].Value = "";


        //        if (DR[i]["BILL_REMARKS"] != null)
        //        {
        //            item_motion.DGVRPT.Rows[i].Cells["CBillNote"].Value = DR[i]["BILL_REMARKS"].ToString();

        //        }
        //        else
        //            item_motion.DGVRPT.Rows[i].Cells["CBillNote"].Value = "";

        //        //-------------------------------ادخال و اخراج و رصيد-----------------------------
        //        if (Convert.ToInt32(Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()).ToString()) == 1 || Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()) == 4 || Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()) == 5 || Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()) == 9)
        //        {

        //            //--------------------------------كمية
        //            if (DR[i]["QTY"] != null)
        //                if (DR[i]["QTY"].ToString() != "")
        //                {
        //                    item_motion.DGVRPT.Rows[i].Cells["CIBillQty"].Value = system.set_QtyPoint(DR[i]["QTY"].ToString());
        //                    CIBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CIBillQty"].Value);


        //                }
        //                else
        //                {
        //                    item_motion.DGVRPT.Rows[i].Cells["CIBillQty"].Value = "";

        //                }
        //            else
        //                item_motion.DGVRPT.Rows[i].Cells["CIBillQty"].Value = "";

        //            //-------------------------------سعر
        //            if (DR[i]["UNIT_PRICE"] != null)
        //                if (DR[i]["UNIT_PRICE"].ToString() != "")
        //                {
        //                    item_motion.DGVRPT.Rows[i].Cells["CIBillPrice"].Value = system.set_PricePoint(DR[i]["UNIT_PRICE"].ToString());
        //                    item_motion.DGVRPT.Rows[i].Cells["CIBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDouble(DR[i]["UNIT_PRICE"].ToString()) * Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                    CIBillTotal += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CIBillTotal"].Value);

        //                }
        //                else
        //                {
        //                    item_motion.DGVRPT.Rows[i].Cells["CIBillPrice"].Value = "";
        //                    item_motion.DGVRPT.Rows[i].Cells["CIBillTotal"].Value = "";

        //                }
        //            else
        //            {
        //                item_motion.DGVRPT.Rows[i].Cells["CIBillPrice"].Value = "";
        //                item_motion.DGVRPT.Rows[i].Cells["CIBillTotal"].Value = "";

        //            }
        //            if (i != 0)
        //            {
        //                if (DR[i]["QTY"] != null)
        //                    if (DR[i]["QTY"].ToString() != "")
        //                    {
        //                        if (DR[i]["UNIT_TRANS_RATE"] != null)
        //                        {
        //                            if (DR[i]["UNIT_TRANS_RATE"].ToString().Trim() != "")
        //                            {
        //                                item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDecimal(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillQty"].Value.ToString()) + Convert.ToDecimal(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate)));
        //                                if (item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value != null)
        //                                {
        //                                    if (item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value.ToString() != "")
        //                                    {
        //                                        if (item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value.ToString().Contains("-") == true)
        //                                        {
        //                                            CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value.ToString().Replace('-', ' ').Trim()) * -1;
        //                                        }
        //                                        else
        //                                        {
        //                                            CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value.ToString());

        //                                        }
        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {
        //                                item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDecimal(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillQty"].Value.ToString()) + Convert.ToDecimal(Convert.ToDouble(DR[i]["QTY"].ToString()))));
        //                                CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);


        //                            }
        //                        }
        //                        else
        //                        {
        //                            item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDecimal(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillQty"].Value.ToString()) + Convert.ToDecimal(Convert.ToDouble(DR[i]["QTY"].ToString()))));
        //                            CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);


        //                        }


        //                    }
        //                if (DR[i]["UNIT_PRICE"] != null)
        //                    if (DR[i]["UNIT_PRICE"].ToString() != "")
        //                    {
        //                        total = total + Convert.ToDouble(DR[i]["UNIT_PRICE"].ToString()) * Convert.ToDouble(DR[i]["QTY"].ToString());
        //                        qty = qty + Convert.ToDouble(DR[i]["QTY"].ToString());
        //                        // item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value.ToString();
        //                        if (qty != 0)
        //                        {
        //                            mid = total / qty;
        //                        }
        //                        else
        //                        {
        //                            mid = total;
        //                        }
        //                        double previousetotal = Convert.ToDouble(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillTotal"].Value);
        //                        double currenttotal = Convert.ToDouble(DR[i]["UNIT_PRICE"].ToString()) * Convert.ToDouble(DR[i]["QTY"].ToString()); ;
        //                        double previouseqty = Convert.ToDouble(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillQty"].Value);
        //                        if ((previouseqty + Convert.ToDouble(DR[i]["QTY"].ToString())) != 0)
        //                        {
        //                            mid = (previousetotal + currenttotal) / (previouseqty + Convert.ToDouble(DR[i]["QTY"].ToString()));
        //                        }
        //                        else
        //                        {
        //                            if (i > 0)
        //                            {
        //                                if (item_motion.DGVRPT.Rows[i - 1].Cells["CTBillPrice"].Value != null)
        //                                {
        //                                    if (Microsoft.VisualBasic.Information.IsNumeric(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillPrice"].Value) == true)
        //                                    {
        //                                        mid = Convert.ToDouble(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillPrice"].Value);
        //                                    }
        //                                }
        //                            }
        //                            else
        //                            {
        //                                mid = 0;
        //                            }

        //                        }
        //                        item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(mid));
        //                        item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDecimal(item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value.ToString()) * Convert.ToDecimal(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value.ToString())));
        //                        CTBillTotal += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value);




        //                    }
        //                    else
        //                    {
        //                        item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillPrice"].Value));
        //                        item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillTotal"].Value));
        //                        CTBillTotal += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value);


        //                    }
        //                else
        //                {
        //                    item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillPrice"].Value));
        //                    item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillTotal"].Value));
        //                    CTBillTotal += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value);



        //                }

        //            }
        //            else
        //            {
        //                if (DR[i]["QTY"] != null)
        //                    if (DR[i]["QTY"].ToString() != "")
        //                    {
        //                        if (DR[i]["UNIT_TRANS_RATE"] != null)
        //                        {

        //                            if (DR[i]["UNIT_TRANS_RATE"].ToString().Trim() != "")
        //                            {
        //                                item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate)));
        //                                CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);

        //                            }
        //                            else
        //                            {
        //                                item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString()))));
        //                                CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);

        //                            }
        //                        }
        //                        else
        //                        {
        //                            item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString()))));
        //                            CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);

        //                        }

        //                    }

        //                if (DR[i]["UNIT_PRICE"] != null)
        //                    if (DR[i]["UNIT_PRICE"].ToString() != "")
        //                    {
        //                        item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(DR[i]["UNIT_PRICE"]));
        //                        item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDecimal(item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value.ToString()) * Convert.ToDecimal(DR[i]["QTY"].ToString())));
        //                        CTBillTotal += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value);



        //                        total = total + Convert.ToDouble(DR[i]["UNIT_PRICE"].ToString()) * Convert.ToDouble(DR[i]["QTY"].ToString());
        //                        qty = qty + Convert.ToDouble(DR[i]["QTY"].ToString());
        //                        if (qty != 0)
        //                        {
        //                            mid = total / qty;
        //                        }
        //                        else
        //                        {
        //                            mid = total;
        //                        }
        //                    }
        //            }

        //        }
        //        else if (Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()) == 2 || Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()) == 3 || Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()) == 6)
        //        {
        //            try
        //            {
        //                if (DR[i]["ACC_CODE"] != null)
        //                {


        //                    if (DR[i]["ACC_AR_NAME"] != null)
        //                    {
        //                        item_motion.DGVRPT.Rows[i].Cells["COBillCUSTACC"].Value = DR[i]["ACC_CODE"].ToString() + "-" + DR[i]["ACC_AR_NAME"].ToString();
        //                    }
        //                }

        //            }
        //            catch (Exception ex)
        //            {

        //            }
        //            //--------------------------------كمية
        //            if (DR[i]["QTY"] != null)
        //                if (DR[i]["QTY"].ToString() != "")
        //                {
        //                    item_motion.DGVRPT.Rows[i].Cells["COBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate)));//system.set_QtyPoint(Convert.ToString(Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                    COBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["COBillQty"].Value);
        //                }
        //                else
        //                {
        //                    item_motion.DGVRPT.Rows[i].Cells["COBillQty"].Value = "";

        //                }
        //            else
        //                item_motion.DGVRPT.Rows[i].Cells["COBillQty"].Value = "";

        //            //-------------------------------سعر
        //            if (DR[i]["UNIT_PRICE"] != null)
        //                if (DR[i]["UNIT_PRICE"].ToString() != "")
        //                {
        //                    item_motion.DGVRPT.Rows[i].Cells["COBillPrice"].Value = system.set_PricePoint(Convert.ToString(DR[i]["UNIT_PRICE"]));
        //                    item_motion.DGVRPT.Rows[i].Cells["COBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDouble(DR[i]["UNIT_PRICE"].ToString()) * Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                    COBillTotal += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["COBillTotal"].Value);


        //                }
        //                else
        //                {
        //                    item_motion.DGVRPT.Rows[i].Cells["COBillPrice"].Value = "";
        //                    item_motion.DGVRPT.Rows[i].Cells["COBillTotal"].Value = "";

        //                }
        //            else
        //            {
        //                item_motion.DGVRPT.Rows[i].Cells["COBillPrice"].Value = "";
        //                item_motion.DGVRPT.Rows[i].Cells["COBillTotal"].Value = "";

        //            }
        //            if (i != 0)
        //            {
        //                if (DR[i]["QTY"] != null)
        //                    if (DR[i]["QTY"].ToString() != "")
        //                        if (DR[i]["UNIT_TRANS_RATE"] != null)
        //                        {
        //                            if (DR[i]["UNIT_TRANS_RATE"].ToString().Trim() != "")
        //                            {
        //                                item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = Convert.ToString(Convert.ToDecimal(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillQty"].Value) - Convert.ToDecimal(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate));
        //                                CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);

        //                            }
        //                            else
        //                            {
        //                                item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = Convert.ToString(Convert.ToDecimal(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillQty"].Value) - Convert.ToDecimal(Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                                CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);


        //                            }
        //                        }
        //                        else
        //                        {
        //                            item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = Convert.ToString(Convert.ToDecimal(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillQty"].Value) - Convert.ToDecimal(Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                            CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);


        //                        }


        //                if (DR[i]["UNIT_PRICE"] != null)
        //                    if (DR[i]["UNIT_PRICE"].ToString() != "")
        //                    {
        //                        item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value = item_motion.DGVRPT.Rows[i - 1].Cells["CTBillPrice"].Value;// system.set_PricePoint(Convert.ToString(Convert.ToDouble(DR[i]["UNIT_PRICE"].ToString())));
        //                        item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDecimal(Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value.ToString())) * Convert.ToDecimal(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value.ToString())));
        //                        CTBillTotal += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value);

        //                    }
        //                    else
        //                    {
        //                        item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(mid));
        //                        item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDecimal(item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value.ToString()) * Convert.ToDecimal(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value.ToString())));
        //                        CTBillTotal += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value);


        //                    }
        //                else
        //                {
        //                    item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(mid));
        //                    item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDecimal(item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value.ToString()) * Convert.ToDecimal(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value.ToString())));
        //                    CTBillTotal += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value);


        //                }
        //            }
        //            else
        //            {
        //                if (DR[i]["QTY"] != null)
        //                    if (DR[i]["QTY"].ToString() != "")
        //                        if (DR[i]["UNIT_TRANS_RATE"] != null)
        //                        {
        //                            if (DR[i]["UNIT_TRANS_RATE"].ToString().Trim() != "")
        //                            {
        //                                item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate) * -1);
        //                                CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);

        //                            }
        //                            else
        //                            {
        //                                item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString())) * -1);
        //                                CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);


        //                            }
        //                        }
        //                        else
        //                        {
        //                            item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString())) * -1);
        //                            CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);


        //                        }


        //                if (DR[i]["UNIT_PRICE"] != null)
        //                    if (DR[i]["UNIT_PRICE"].ToString() != "")
        //                    {
        //                        item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(DR[i]["UNIT_PRICE"].ToString()));
        //                        item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value.ToString()) * Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                        CTBillTotal += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value);


        //                    }
        //            }

        //        }
        //        else if (Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()) == 11 || Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()) == 12)
        //        {
        //            //--------------------------------كمية
        //            if (DR[i]["QTY"] != null)
        //                if (DR[i]["QTY"].ToString() != "")
        //                {
        //                    if (DR[i]["UNIT_TRANS_RATE"] != null)
        //                    {
        //                        //item_motion.DGVRPT.Rows[i].Cells["COBillQty"].Value = system.set_QtyPoint((Convert.ToDouble(DR[i]["QTY"].ToString())*Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString())/_Unit_Transfer_Rate).ToString());
        //                        if (DR[i]["TO_COM_STORE_ID"] != null)
        //                        {
        //                            if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() != "")
        //                            {
        //                                if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                                {
        //                                    item_motion.DGVRPT.Rows[i].Cells["CIBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate)));// system.set_QtyPoint(DR[i]["QTY"].ToString());
        //                                    CIBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CIBillQty"].Value);


        //                                }
        //                            }
        //                        }


        //                        if (DR[i]["COM_STORE_ID"] != null)
        //                        {
        //                            if (DR[i]["COM_STORE_ID"].ToString().Trim() != "")
        //                            {
        //                                if (DR[i]["COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                                {
        //                                    item_motion.DGVRPT.Rows[i].Cells["COBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate)));// system.set_QtyPoint(DR[i]["QTY"].ToString());
        //                                    COBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["COBillQty"].Value);


        //                                }
        //                            }
        //                        }

        //                    }
        //                    else
        //                    {
        //                        //item_motion.DGVRPT.Rows[i].Cells["COBillQty"].Value = system.set_QtyPoint((Convert.ToDouble(DR[i]["QTY"].ToString())*Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString())/_Unit_Transfer_Rate).ToString());
        //                        if (DR[i]["TO_COM_STORE_ID"] != null)
        //                        {
        //                            if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() != "")
        //                            {
        //                                if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                                {
        //                                    item_motion.DGVRPT.Rows[i].Cells["CIBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate)));// system.set_QtyPoint(DR[i]["QTY"].ToString());
        //                                    CIBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CIBillQty"].Value);


        //                                }
        //                            }
        //                        }


        //                        if (DR[i]["COM_STORE_ID"] != null)
        //                        {
        //                            if (DR[i]["COM_STORE_ID"].ToString().Trim() != "")
        //                            {
        //                                if (DR[i]["COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                                {
        //                                    item_motion.DGVRPT.Rows[i].Cells["COBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate)));// system.set_QtyPoint(DR[i]["QTY"].ToString());
        //                                    COBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["COBillQty"].Value);


        //                                }
        //                            }
        //                        }
        //                    }

        //                }
        //                else
        //                {
        //                    if (DR[i]["TO_COM_STORE_ID"] != null)
        //                    {
        //                        if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() != "")
        //                        {
        //                            if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                            {
        //                                item_motion.DGVRPT.Rows[i].Cells["CIBillQty"].Value = "";
        //                            }
        //                        }
        //                    }


        //                    if (DR[i]["COM_STORE_ID"] != null)
        //                    {
        //                        if (DR[i]["COM_STORE_ID"].ToString().Trim() != "")
        //                        {
        //                            if (DR[i]["COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                            {
        //                                item_motion.DGVRPT.Rows[i].Cells["COBillQty"].Value = "";
        //                                COBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["COBillQty"].Value);


        //                            }
        //                        }
        //                    }

        //                }

        //            //-------------------------------سعر
        //            if (DR[i]["UNIT_PRICE"] != null)
        //                if (DR[i]["UNIT_PRICE"].ToString() != "")
        //                {

        //                    if (DR[i]["COM_STORE_ID"] != null)
        //                    {
        //                        if (DR[i]["COM_STORE_ID"].ToString().Trim() != "")
        //                        {
        //                            if (DR[i]["COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                            {
        //                                item_motion.DGVRPT.Rows[i].Cells["COBillPrice"].Value = system.set_PricePoint(Convert.ToString(DR[i]["UNIT_PRICE"]));
        //                            }
        //                        }
        //                    }
        //                    if (DR[i]["TO_COM_STORE_ID"] != null)
        //                    {
        //                        if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() != "")
        //                        {
        //                            if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                            {
        //                                item_motion.DGVRPT.Rows[i].Cells["CIBillPrice"].Value = system.set_PricePoint(Convert.ToString(DR[i]["UNIT_PRICE"]));
        //                            }
        //                        }
        //                    }

        //                    if (DR[i]["COM_STORE_ID"] != null)
        //                    {
        //                        if (DR[i]["COM_STORE_ID"].ToString().Trim() != "")
        //                        {
        //                            if (DR[i]["COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                            {
        //                                item_motion.DGVRPT.Rows[i].Cells["COBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDouble(DR[i]["UNIT_PRICE"].ToString()) * Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                                COBillTotal += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["COBillTotal"].Value);

        //                            }
        //                        }
        //                    }
        //                    if (DR[i]["TO_COM_STORE_ID"] != null)
        //                    {
        //                        if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() != "")
        //                        {
        //                            if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                            {
        //                                item_motion.DGVRPT.Rows[i].Cells["CIBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDouble(DR[i]["UNIT_PRICE"].ToString()) * Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                                CIBillTotal += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CIBillTotal"].Value);

        //                            }
        //                        }
        //                    }


        //                }
        //                else
        //                {
        //                    item_motion.DGVRPT.Rows[i].Cells["COBillPrice"].Value = "";
        //                    item_motion.DGVRPT.Rows[i].Cells["CIBillPrice"].Value = "";
        //                    item_motion.DGVRPT.Rows[i].Cells["COBillTotal"].Value = "";
        //                    item_motion.DGVRPT.Rows[i].Cells["CIBillTotal"].Value = "";


        //                }
        //            else
        //            {
        //                item_motion.DGVRPT.Rows[i].Cells["COBillPrice"].Value = "";
        //                item_motion.DGVRPT.Rows[i].Cells["CIBillPrice"].Value = "";
        //                item_motion.DGVRPT.Rows[i].Cells["COBillTotal"].Value = "";
        //                item_motion.DGVRPT.Rows[i].Cells["CIBillTotal"].Value = "";


        //            }

        //            if (i != 0)
        //            {
        //                item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = "0";
        //                item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value = "0";
        //                item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value = "0";
        //                if (DR[i]["TO_COM_STORE_ID"] != null)
        //                {
        //                    if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() != "")
        //                    {
        //                        if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                        {
        //                            if (DR[i]["UNIT_TRANS_RATE"] != null)
        //                            {
        //                                if (DR[i]["UNIT_TRANS_RATE"].ToString().Trim() != "")
        //                                {
        //                                    item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDecimal(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillQty"].Value.ToString()) + Convert.ToDecimal(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate)));
        //                                    CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value.ToString().Replace('-', ' ').Trim()) * -1;
        //                                    // CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);

        //                                }
        //                                else
        //                                {
        //                                    item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDecimal(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillQty"].Value.ToString()) + Convert.ToDecimal(Convert.ToDouble(DR[i]["QTY"].ToString()))));
        //                                    CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);


        //                                }
        //                            }
        //                            else
        //                            {
        //                                item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDecimal(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillQty"].Value.ToString()) + Convert.ToDecimal(Convert.ToDouble(DR[i]["QTY"].ToString()))));
        //                                CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);


        //                            }
        //                        }
        //                    }
        //                }
        //                if (DR[i]["COM_STORE_ID"] != null)
        //                {
        //                    if (DR[i]["COM_STORE_ID"].ToString().Trim() != "")
        //                    {
        //                        if (DR[i]["COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                        {
        //                            if (DR[i]["UNIT_TRANS_RATE"] != null)
        //                            {
        //                                if (DR[i]["UNIT_TRANS_RATE"].ToString().Trim() != "")
        //                                {
        //                                    item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = Convert.ToString(Convert.ToDecimal(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillQty"].Value.ToString()) - Convert.ToDecimal(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate));
        //                                    CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);

        //                                }
        //                                else
        //                                {
        //                                    item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = Convert.ToString(Convert.ToDecimal(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillQty"].Value.ToString()) - Convert.ToDecimal(Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                                    CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);


        //                                }
        //                            }
        //                            else
        //                            {
        //                                item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = Convert.ToString(Convert.ToDecimal(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillQty"].Value.ToString()) - Convert.ToDecimal(Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                                CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);


        //                            }
        //                        }
        //                    }
        //                }

        //                //    item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = system.set_PricePoint(Convert.ToString(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillQty"].Value));
        //                item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillPrice"].Value));
        //                item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillTotal"].Value));
        //                CTBillTotal += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value);



        //            }
        //            else
        //            {
        //                if (DR[i]["QTY"] != null)
        //                    if (DR[i]["QTY"].ToString() != "")
        //                    {
        //                        item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) * Convert.ToDouble(DR[i]["QTY"].ToString()) / _Unit_Transfer_Rate));
        //                        //item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDecimal(item_motion.DGVRPT.Rows[i - 1].Cells["CTBillQty"].Value.ToString()) + Convert.ToDecimal(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate)));
        //                        CTBillQty += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillQty"].Value);

        //                    }


        //                if (DR[i]["UNIT_PRICE"] != null)
        //                    if (DR[i]["UNIT_PRICE"].ToString() != "")
        //                    {
        //                        item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(DR[i]["UNIT_PRICE"]));
        //                        item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillPrice"].Value.ToString()) * Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                        CTBillTotal += Convert.ToDouble(item_motion.DGVRPT.Rows[i].Cells["CTBillTotal"].Value);



        //                    }
        //            }



        //        }
        //        //--------------------------------------------الهدايا
        //        if (CHBGift.Checked)
        //        {
        //            if (DR[i]["GIFT"] != null)
        //            {
        //                item_motion.DGVRPT.Rows[i].Cells["CBillGift"].Value = system.set_QtyPoint(DR[i]["GIFT"].ToString().Trim());

        //            }
        //        }
        //        //--------------------------------------------الخصومات والإضافات
        //        if (CHBDiscExtraView.Checked)
        //        {
        //            if (DR[i]["EXTRA_VALUE"] != null)
        //                item_motion.DGVRPT.Rows[i].Cells["CBillExtra"].Value = system.set_PricePoint(DR[i]["EXTRA_VALUE"].ToString());

        //            if (DR[i]["EXTRA_RATE"] != null)
        //                item_motion.DGVRPT.Rows[i].Cells["CBillExtraRate"].Value = system.set_PricePoint(DR[i]["EXTRA_RATE"].ToString());


        //            if (DR[i]["DISC_VALUE"] != null)
        //                item_motion.DGVRPT.Rows[i].Cells["CBillDisc"].Value = system.set_PricePoint(DR[i]["DISC_VALUE"].ToString());

        //            if (DR[i]["DISC_RATE"] != null)
        //                item_motion.DGVRPT.Rows[i].Cells["CBillDiscRate"].Value = system.set_PricePoint(DR[i]["DISC_RATE"].ToString());

        //        }
        //        //---------------------bill id
        //        if (DR[i]["BILL_ID"] != null)
        //            item_motion.Bill_id.Add(DR[i]["BILL_ID"]);
        //        else
        //            item_motion.Bill_id.Add("");

        //        //----------------------bill type
        //        if (DR[i]["BILL_TYPE"] != null)
        //            item_motion.Bill_type.Add(DR[i]["BILL_TYPE"]);
        //        else
        //            item_motion.Bill_type.Add("");

        //    }

        //    //return true;

        //    for (int i = 0; i <= DR.Length - 1; i++)
        //    {

        //        item_motion.dataGridView1.Rows.Add(1);

        //        if (MainParent.lang == "A")
        //        {

        //            item_motion.dataGridView1.Rows[i].Cells["CBillNumber"].Value = DR[i]["BILL_ABRV_AR"].ToString() + ":" + DR[i]["BILL_NUMBER"].ToString();

        //            //-----------------------------------------الوحدة
        //            if (DR[i]["UNIT_CODE"] != null)
        //            {
        //                if (DR[i]["UNIT_CODE"].ToString() != "")
        //                {
        //                    item_motion.dataGridView1.Rows[i].Cells["CBillUnit"].Value = DR[i]["UNIT_CODE"].ToString().Trim() + "-" + DR[i]["UNIT_AR_NAME"].ToString().Trim();

        //                }
        //            }
        //            //-----------------------------------------المخزن
        //            if (CHBStoreView.Checked)
        //            {
        //                if (DR[i]["COM_STORE_CODE"] != null)
        //                {
        //                    if (DR[i]["COM_STORE_CODE"].ToString() != "")
        //                    {
        //                        item_motion.dataGridView1.Rows[i].Cells["CBillStore"].Value = DR[i]["COM_STORE_CODE"].ToString().Trim() + "-" + DR[i]["COM_STORE_AR_NAME"].ToString().Trim();

        //                    }
        //                }
        //            }
        //            //---------------------------------------البائع
        //            if (CHBEmployeeView.Checked)
        //            {
        //                if (DR[i]["EMP_CODE"] != null)
        //                {
        //                    if (DR[i]["EMP_CODE"].ToString() != "")
        //                    {
        //                        item_motion.dataGridView1.Rows[i].Cells["CBillEmployee"].Value = DR[i]["EMP_CODE"].ToString().Trim() + "-" + DR[i]["EMP_AR_NAME"].ToString().Trim();

        //                    }
        //                }
        //            }
        //            //----------------------bill text
        //            if (DR[i]["BILL_AR_NAME"] != null)
        //                item_motion.Bill_text.Add(DR[i]["BILL_AR_NAME"]);
        //            else
        //                item_motion.Bill_text.Add("");
        //        }
        //        else
        //        {
        //            item_motion.dataGridView1.Rows[i].Cells["CBillNumber"].Value = DR[i]["BILL_ABRV_EN"].ToString() + ":" + DR[i]["BILL_NUMBER"].ToString();

        //            //-----------------------------------------الوحدة
        //            if (DR[i]["UNIT_CODE"] != null)
        //            {
        //                if (DR[i]["UNIT_CODE"].ToString() != "")
        //                {
        //                    item_motion.dataGridView1.Rows[i].Cells["CBillUnit"].Value = DR[i]["UNIT_CODE"].ToString().Trim() + "-" + DR[i]["UNIT_EN_NAME"].ToString().Trim();

        //                }
        //            }
        //            //-----------------------------------------المخزن
        //            if (CHBStoreView.Checked)
        //            {
        //                if (DR[i]["COM_STORE_CODE"] != null)
        //                {
        //                    if (DR[i]["COM_STORE_CODE"].ToString() != "")
        //                    {
        //                        item_motion.dataGridView1.Rows[i].Cells["CBillStore"].Value = DR[i]["COM_STORE_CODE"].ToString().Trim() + "-" + DR[i]["COM_STORE_EN_NAME"].ToString().Trim();

        //                    }
        //                }
        //            }
        //            //----------------------------------------البائع
        //            if (CHBEmployeeView.Checked)
        //            {
        //                if (DR[i]["EMP_CODE"] != null)
        //                {
        //                    if (DR[i]["EMP_CODE"].ToString() != "")
        //                    {
        //                        item_motion.dataGridView1.Rows[i].Cells["CBillEmployee"].Value = DR[i]["EMP_CODE"].ToString().Trim() + "-" + DR[i]["EMP_EN_NAME"].ToString().Trim();

        //                    }
        //                }
        //            }
        //            //----------------------bill text
        //            if (DR[i]["BILL_EN_NAME"] != null)
        //                item_motion.Bill_text.Add(DR[i]["BILL_EN_NAME"]);
        //            else
        //                item_motion.Bill_text.Add("");
        //        }
        //        if (DR[i]["BILL_DATE"] != null)
        //        {
        //            item_motion.dataGridView1.Rows[i].Cells["CBillDate"].Value = Convert.ToDateTime(DR[i]["BILL_DATE"].ToString()).ToString("yyyy/MM/dd");

        //        }
        //        else
        //            item_motion.dataGridView1.Rows[i].Cells["CBillDate"].Value = "";

        //        if (DR[i]["BILL_REMARKS"] != null)
        //        {
        //            item_motion.dataGridView1.Rows[i].Cells["CBillNote"].Value = DR[i]["BILL_REMARKS"].ToString();

        //        }
        //        else

        //            item_motion.dataGridView1.Rows[i].Cells["CBillNote"].Value = "";
        //        //-------------------------------ادخال و اخراج و رصيد-----------------------------
        //        if (Convert.ToInt32(Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()).ToString()) == 1 || Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()) == 4 || Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()) == 5 || Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()) == 9)
        //        {

        //            //--------------------------------كمية
        //            if (DR[i]["QTY"] != null)
        //                if (DR[i]["QTY"].ToString() != "")
        //                {
        //                    if (DR[i]["UNIT_TRANS_RATE"] != null)
        //                    {
        //                        if (DR[i]["UNIT_TRANS_RATE"].ToString().Trim() != "")
        //                        {
        //                            item_motion.dataGridView1.Rows[i].Cells["CIBillQty"].Value = system.set_QtyPoint((Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate).ToString());
        //                            // CIBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CIBillQty"].Value);

        //                        }
        //                        else
        //                        {
        //                            item_motion.dataGridView1.Rows[i].Cells["CIBillQty"].Value = system.set_QtyPoint(DR[i]["QTY"].ToString());
        //                            //  CIBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CIBillQty"].Value);


        //                        }

        //                    }
        //                    else
        //                    {
        //                        item_motion.dataGridView1.Rows[i].Cells["CIBillQty"].Value = system.set_QtyPoint(DR[i]["QTY"].ToString());
        //                        // CIBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CIBillQty"].Value);


        //                    }
        //                    // item_motion.dataGridView1.Rows[i].Cells["CIBillQty"].Value = system.set_QtyPoint(DR[i]["QTY"].ToString());

        //                }
        //                else
        //                {

        //                    item_motion.dataGridView1.Rows[i].Cells["CIBillQty"].Value = "";
        //                }
        //            else

        //                item_motion.dataGridView1.Rows[i].Cells["CIBillQty"].Value = "";
        //            //-------------------------------سعر
        //            if (DR[i]["UNIT_PRICE"] != null)
        //                if (DR[i]["UNIT_PRICE"].ToString() != "")
        //                {
        //                    item_motion.dataGridView1.Rows[i].Cells["CIBillPrice"].Value = system.set_PricePoint(DR[i]["UNIT_PRICE"].ToString());
        //                    item_motion.dataGridView1.Rows[i].Cells["CIBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDouble(DR[i]["UNIT_PRICE"].ToString()) * Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                    // CIBillTotal += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CIBillTotal"].Value);


        //                }
        //                else
        //                {

        //                    item_motion.dataGridView1.Rows[i].Cells["CIBillPrice"].Value = "";
        //                    item_motion.dataGridView1.Rows[i].Cells["CIBillTotal"].Value = "";
        //                }
        //            else
        //            {

        //                item_motion.dataGridView1.Rows[i].Cells["CIBillPrice"].Value = "";
        //                item_motion.dataGridView1.Rows[i].Cells["CIBillTotal"].Value = "";
        //            }
        //            if (i != 0)
        //            {
        //                if (DR[i]["QTY"] != null)
        //                    if (DR[i]["QTY"].ToString() != "")
        //                    {
        //                        if (DR[i]["UNIT_TRANS_RATE"] != null)
        //                        {
        //                            if (DR[i]["UNIT_TRANS_RATE"].ToString().Trim() != "")
        //                            {
        //                                item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value = Convert.ToString(Convert.ToDecimal(item_motion.dataGridView1.Rows[i - 1].Cells["CTBillQty"].Value.ToString()) + (Convert.ToDecimal(DR[i]["QTY"].ToString()) * Convert.ToDecimal(DR[i]["UNIT_TRANS_RATE"].ToString())));
        //                                //   CTBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value);

        //                            }
        //                            else
        //                            {
        //                                item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDecimal(item_motion.dataGridView1.Rows[i - 1].Cells["CTBillQty"].Value.ToString()) + (Convert.ToDecimal(DR[i]["QTY"].ToString()))));
        //                                //CTBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value);

        //                            }
        //                        }
        //                        else
        //                        {
        //                            item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDecimal(item_motion.dataGridView1.Rows[i - 1].Cells["CTBillQty"].Value.ToString()) + (Convert.ToDecimal(DR[i]["QTY"].ToString()))));
        //                            //   CTBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value);

        //                        }
        //                        //                                    item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDecimal(item_motion.dataGridView1.Rows[i - 1].Cells["CTBillQty"].Value.ToString()) + Convert.ToDecimal(DR[i]["QTY"].ToString())));


        //                    }
        //                if (DR[i]["UNIT_PRICE"] != null)
        //                    if (DR[i]["UNIT_PRICE"].ToString() != "")
        //                    {
        //                        total = total + Convert.ToDouble(DR[i]["UNIT_PRICE"].ToString()) * Convert.ToDouble(DR[i]["QTY"].ToString());
        //                        qty = qty + Convert.ToDouble(DR[i]["QTY"].ToString());

        //                        if (qty != 0)
        //                        {
        //                            mid = total / qty;
        //                        }
        //                        else
        //                        {
        //                            mid = total;
        //                        }
        //                        item_motion.dataGridView1.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(mid));
        //                        item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDecimal(mid) * Convert.ToDecimal(item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value.ToString())));
        //                        // CTBillTotal += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value);



        //                    }
        //                    else
        //                    {
        //                        item_motion.dataGridView1.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(item_motion.dataGridView1.Rows[i - 1].Cells["CTBillPrice"].Value));
        //                        item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(item_motion.dataGridView1.Rows[i - 1].Cells["CTBillTotal"].Value));
        //                        //    CTBillTotal += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value);



        //                    }
        //                else
        //                {
        //                    item_motion.dataGridView1.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(item_motion.dataGridView1.Rows[i - 1].Cells["CTBillPrice"].Value));
        //                    item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(item_motion.dataGridView1.Rows[i - 1].Cells["CTBillTotal"].Value));
        //                    // CTBillTotal += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value);


        //                }

        //            }
        //            else
        //            {
        //                if (DR[i]["QTY"] != null)
        //                    if (DR[i]["QTY"].ToString() != "")
        //                    {
        //                        if (DR[i]["UNIT_TRANS_RATE"] != null)
        //                        {
        //                            if (DR[i]["UNIT_TRANS_RATE"].ToString().Trim() != "")
        //                            {
        //                                item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate));
        //                                // CTBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value);

        //                            }
        //                            else
        //                            {
        //                                item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                                // CTBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value);


        //                            }
        //                        }
        //                        else
        //                        {
        //                            item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                            //CTBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value);


        //                        }
        //                    }

        //                if (DR[i]["UNIT_PRICE"] != null)
        //                    if (DR[i]["UNIT_PRICE"].ToString() != "")
        //                    {
        //                        item_motion.dataGridView1.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(DR[i]["UNIT_PRICE"]));
        //                        item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDecimal(DR[i]["UNIT_PRICE"].ToString()) * Convert.ToDecimal(DR[i]["QTY"].ToString())));

        //                        // CTBillTotal += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value);


        //                        total = total + Convert.ToDouble(DR[i]["UNIT_PRICE"].ToString()) * Convert.ToDouble(DR[i]["QTY"].ToString());
        //                        qty = qty + Convert.ToDouble(DR[i]["QTY"].ToString());
        //                        if (qty != 0)
        //                        {
        //                            mid = total / qty;
        //                        }
        //                        else
        //                        {
        //                            mid = total;
        //                        }
        //                    }
        //            }

        //        }
        //        else if (Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()) == 2 || Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()) == 3 || Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()) == 6)
        //        {
        //            //--------------------------------كمية
        //            if (DR[i]["QTY"] != null)
        //                if (DR[i]["QTY"].ToString() != "")
        //                {
        //                    if (DR[i]["UNIT_TRANS_RATE"] != null)
        //                    {
        //                        if (DR[i]["UNIT_TRANS_RATE"].ToString().Trim() != "")
        //                        {
        //                            item_motion.dataGridView1.Rows[i].Cells["COBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate));
        //                            //  COBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["COBillQty"].Value);

        //                        }
        //                        else
        //                        {
        //                            item_motion.dataGridView1.Rows[i].Cells["COBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate)));// system.set_QtyPoint(Convert.ToString(Convert.ToDouble(DR[i]["QTY"].ToString())));

        //                            // COBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["COBillQty"].Value);

        //                        }
        //                    }
        //                    else
        //                    {
        //                        item_motion.dataGridView1.Rows[i].Cells["COBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate)));//system.set_QtyPoint(Convert.ToString(Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                                                                                                                                                                                                                                                                      // COBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["COBillQty"].Value);


        //                    }

        //                }
        //                else
        //                {

        //                    item_motion.dataGridView1.Rows[i].Cells["COBillQty"].Value = "";
        //                }
        //            else

        //                item_motion.dataGridView1.Rows[i].Cells["COBillQty"].Value = "";
        //            //-------------------------------سعر
        //            if (DR[i]["UNIT_PRICE"] != null)
        //                if (DR[i]["UNIT_PRICE"].ToString() != "")
        //                {
        //                    item_motion.dataGridView1.Rows[i].Cells["COBillPrice"].Value = system.set_PricePoint(Convert.ToString(DR[i]["UNIT_PRICE"]));
        //                    item_motion.dataGridView1.Rows[i].Cells["COBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDouble(DR[i]["UNIT_PRICE"].ToString()) * Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                    // COBillTotal += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["COBillTotal"].Value);


        //                }
        //                else
        //                {

        //                    item_motion.dataGridView1.Rows[i].Cells["COBillPrice"].Value = "";
        //                    item_motion.dataGridView1.Rows[i].Cells["COBillTotal"].Value = "";
        //                }
        //            else
        //            {

        //                item_motion.dataGridView1.Rows[i].Cells["COBillPrice"].Value = "";
        //                item_motion.dataGridView1.Rows[i].Cells["COBillTotal"].Value = "";
        //            }
        //            if (i != 0)
        //            {
        //                if (DR[i]["QTY"] != null)
        //                    if (DR[i]["QTY"].ToString() != "")
        //                        if (DR[i]["UNIT_TRANS_RATE"] != null)
        //                        {
        //                            if (DR[i]["UNIT_TRANS_RATE"].ToString().Trim() != "")
        //                            {
        //                                item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDecimal(item_motion.dataGridView1.Rows[i - 1].Cells["CTBillQty"].Value) - Convert.ToDecimal(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate)));
        //                                //CTBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value);

        //                            }
        //                            else
        //                            {
        //                                item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDecimal(item_motion.dataGridView1.Rows[i - 1].Cells["CTBillQty"].Value) - Convert.ToDecimal(Convert.ToDouble(DR[i]["QTY"].ToString()))));
        //                                // CTBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value);


        //                            }
        //                        }
        //                        else
        //                        {
        //                            item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDecimal(item_motion.dataGridView1.Rows[i - 1].Cells["CTBillQty"].Value) - Convert.ToDecimal(Convert.ToDouble(DR[i]["QTY"].ToString()))));
        //                            // CTBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value);


        //                        }


        //                if (DR[i]["UNIT_PRICE"] != null)
        //                    if (DR[i]["UNIT_PRICE"].ToString() != "")
        //                    {
        //                        item_motion.dataGridView1.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(mid));
        //                        item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDecimal(mid) * Convert.ToDecimal(item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value.ToString())));
        //                        //  CTBillTotal += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value);


        //                    }
        //                    else
        //                    {
        //                        item_motion.dataGridView1.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(mid));
        //                        item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDecimal(mid) * Convert.ToDecimal(item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value.ToString())));
        //                        // CTBillTotal += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value);


        //                    }
        //                else
        //                {
        //                    item_motion.dataGridView1.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(mid));
        //                    item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDecimal(mid) * Convert.ToDecimal(item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value.ToString())));
        //                    //   CTBillTotal += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value);


        //                }
        //            }
        //            else
        //            {
        //                if (DR[i]["QTY"] != null)
        //                    if (DR[i]["QTY"].ToString() != "")
        //                        if (DR[i]["UNIT_TRANS_RATE"] != null)
        //                        {
        //                            if (DR[i]["UNIT_TRANS_RATE"].ToString().Trim() != "")
        //                            {
        //                                item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate) * -1));
        //                                // CTBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value);

        //                            }
        //                            else
        //                            {
        //                                item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString())) * -1));
        //                                // CTBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value);


        //                            }
        //                        }
        //                        else
        //                        {
        //                            item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString())) * -1));
        //                            // CTBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value);


        //                        }

        //                if (DR[i]["UNIT_PRICE"] != null)
        //                    if (DR[i]["UNIT_PRICE"].ToString() != "")
        //                    {
        //                        item_motion.dataGridView1.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(DR[i]["UNIT_PRICE"].ToString()));
        //                        item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDouble(DR[i]["UNIT_PRICE"].ToString()) * Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                        // CTBillTotal += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value);


        //                    }
        //            }

        //            try
        //            {
        //                if (DR[i]["ACC_CODE"] != null)
        //                {


        //                    if (DR[i]["ACC_AR_NAME"] != null)
        //                    {
        //                        item_motion.DGVRPT.Rows[i].Cells["COBillCUSTACC"].Value = DR[i]["ACC_CODE"].ToString() + "-" + DR[i]["ACC_AR_NAME"].ToString();
        //                    }
        //                }

        //            }
        //            catch (Exception ex)
        //            {

        //            }

        //        }
        //        else if (Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()) == 11 || Convert.ToInt32(DR[i]["BILL_TYPE"].ToString()) == 12)
        //        {
        //            //--------------------------------كمية
        //            if (DR[i]["QTY"] != null)
        //                if (DR[i]["QTY"].ToString() != "")
        //                {
        //                    if (DR[i]["COM_STORE_ID"] != null)
        //                    {
        //                        if (DR[i]["COM_STORE_ID"].ToString().Trim() != "")
        //                        {
        //                            if (DR[i]["COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                            {
        //                                if (DR[i]["UNIT_TRANS_RATE"] != null)
        //                                {
        //                                    if (DR[i]["UNIT_TRANS_RATE"].ToString().Trim() != "")
        //                                    {
        //                                        item_motion.dataGridView1.Rows[i].Cells["COBillQty"].Value = system.set_QtyPoint((Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate).ToString());
        //                                        //    COBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["COBillQty"].Value);

        //                                    }
        //                                    else
        //                                    {
        //                                        item_motion.dataGridView1.Rows[i].Cells["COBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate)));//system.set_QtyPoint((Convert.ToDouble(DR[i]["QTY"].ToString()) ).ToString());
        //                                                                                                                                                                                                                                                                                      // COBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["COBillQty"].Value);


        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    item_motion.dataGridView1.Rows[i].Cells["COBillQty"].Value = system.set_QtyPoint(Convert.ToString(Convert.ToDouble(Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate)));// system.set_QtyPoint((Convert.ToDouble(DR[i]["QTY"].ToString())).ToString());
        //                                                                                                                                                                                                                                                                                  //  COBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["COBillQty"].Value);


        //                                }

        //                            }
        //                        }
        //                    }

        //                    if (DR[i]["TO_COM_STORE_ID"] != null)
        //                    {
        //                        if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() != "")
        //                        {
        //                            if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                            {
        //                                if (DR[i]["UNIT_TRANS_RATE"] != null)
        //                                {
        //                                    if (DR[i]["UNIT_TRANS_RATE"].ToString().Trim() != "")
        //                                    {
        //                                        item_motion.dataGridView1.Rows[i].Cells["CIBillQty"].Value = system.set_QtyPoint((Convert.ToDouble(DR[i]["QTY"].ToString()) * Convert.ToDouble(DR[i]["UNIT_TRANS_RATE"].ToString()) / _Unit_Transfer_Rate).ToString());
        //                                        // CIBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CIBillQty"].Value);

        //                                    }
        //                                    else
        //                                    {
        //                                        item_motion.dataGridView1.Rows[i].Cells["CIBillQty"].Value = system.set_QtyPoint((Convert.ToDouble(DR[i]["QTY"].ToString())).ToString());
        //                                        // CIBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CIBillQty"].Value);


        //                                    }
        //                                }
        //                                else
        //                                {
        //                                    item_motion.dataGridView1.Rows[i].Cells["CIBillQty"].Value = system.set_QtyPoint((Convert.ToDouble(DR[i]["QTY"].ToString())).ToString());
        //                                    //  CIBillQty += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CIBillQty"].Value);


        //                                }
        //                            }
        //                        }
        //                    }

        //                }
        //                else
        //                {

        //                    item_motion.dataGridView1.Rows[i].Cells["COBillQty"].Value = "";
        //                    item_motion.dataGridView1.Rows[i].Cells["CIBillQty"].Value = "";
        //                }
        //            else
        //            {

        //                item_motion.dataGridView1.Rows[i].Cells["COBillQty"].Value = "";
        //                item_motion.dataGridView1.Rows[i].Cells["CIBillQty"].Value = "";
        //            }
        //            //-------------------------------سعر
        //            if (DR[i]["UNIT_PRICE"] != null)
        //                if (DR[i]["UNIT_PRICE"].ToString() != "")
        //                {
        //                    if (DR[i]["COM_STORE_ID"] != null)
        //                    {
        //                        if (DR[i]["COM_STORE_ID"].ToString().Trim() != "")
        //                        {
        //                            if (DR[i]["COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                            {
        //                                item_motion.dataGridView1.Rows[i].Cells["COBillPrice"].Value = system.set_PricePoint(Convert.ToString(DR[i]["UNIT_PRICE"]));
        //                            }
        //                        }
        //                    }
        //                    if (DR[i]["TO_COM_STORE_ID"] != null)
        //                    {
        //                        if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() != "")
        //                        {
        //                            if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                            {
        //                                item_motion.dataGridView1.Rows[i].Cells["CIBillPrice"].Value = system.set_PricePoint(Convert.ToString(DR[i]["UNIT_PRICE"]));
        //                            }
        //                        }
        //                    }
        //                    if (DR[i]["COM_STORE_ID"] != null)
        //                    {
        //                        if (DR[i]["COM_STORE_ID"].ToString().Trim() != "")
        //                        {
        //                            if (DR[i]["COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                            {

        //                                item_motion.dataGridView1.Rows[i].Cells["COBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDouble(DR[i]["UNIT_PRICE"].ToString()) * Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                                //  COBillTotal += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["COBillTotal"].Value);

        //                            }
        //                        }
        //                    }
        //                    if (DR[i]["TO_COM_STORE_ID"] != null)
        //                    {
        //                        if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() != "")
        //                        {
        //                            if (DR[i]["TO_COM_STORE_ID"].ToString().Trim() == Com_Store_ID.ToString())
        //                            {
        //                                item_motion.dataGridView1.Rows[i].Cells["CIBillTotal"].Value = system.set_PricePoint(Convert.ToString(Convert.ToDouble(DR[i]["UNIT_PRICE"].ToString()) * Convert.ToDouble(DR[i]["QTY"].ToString())));
        //                                //        CIBillTotal += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CIBillTotal"].Value);

        //                            }
        //                        }
        //                    }



        //                }
        //                else
        //                {


        //                    item_motion.dataGridView1.Rows[i].Cells["COBillPrice"].Value = "";
        //                    item_motion.dataGridView1.Rows[i].Cells["CIBillPrice"].Value = "";
        //                    item_motion.dataGridView1.Rows[i].Cells["COBillTotal"].Value = "";
        //                    item_motion.dataGridView1.Rows[i].Cells["CIBillTotal"].Value = "";
        //                }
        //            else
        //            {

        //                item_motion.dataGridView1.Rows[i].Cells["COBillPrice"].Value = "";
        //                item_motion.dataGridView1.Rows[i].Cells["CIBillPrice"].Value = "";
        //                item_motion.dataGridView1.Rows[i].Cells["COBillTotal"].Value = "";
        //                item_motion.dataGridView1.Rows[i].Cells["CIBillTotal"].Value = "";
        //            }

        //            if (i != 0)
        //            {
        //                item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value = system.set_PricePoint(Convert.ToString(item_motion.dataGridView1.Rows[i - 1].Cells["CTBillQty"].Value));
        //                item_motion.dataGridView1.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint(Convert.ToString(item_motion.dataGridView1.Rows[i - 1].Cells["CTBillPrice"].Value));
        //                item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(item_motion.dataGridView1.Rows[i - 1].Cells["CTBillTotal"].Value));
        //                //   CTBillTotal += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value);



        //            }
        //            else
        //            {
        //                if (DR[i]["QTY"] != null)
        //                    if (DR[i]["QTY"].ToString() != "")
        //                        item_motion.dataGridView1.Rows[i].Cells["CTBillQty"].Value = system.set_QtyPoint("0");

        //                if (DR[i]["UNIT_PRICE"] != null)
        //                    if (DR[i]["UNIT_PRICE"].ToString() != "")
        //                    {
        //                        item_motion.dataGridView1.Rows[i].Cells["CTBillPrice"].Value = system.set_PricePoint("0");
        //                        item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value = system.set_PricePoint("0");
        //                        //  CTBillTotal += Convert.ToDouble(item_motion.dataGridView1.Rows[i].Cells["CTBillTotal"].Value);



        //                    }
        //            }
        //        }
        //        //--------------------------------------------الهدايا
        //        if (CHBGift.Checked)
        //        {
        //            if (DR[i]["GIFT"] != null)
        //            {
        //                item_motion.dataGridView1.Rows[i].Cells["CBillGift"].Value = system.set_QtyPoint(DR[i]["GIFT"].ToString().Trim());

        //            }
        //        }
        //        //--------------------------------------------الخصومات والإضافات
        //        if (CHBDiscExtraView.Checked)
        //        {
        //            if (DR[i]["EXTRA_VALUE"] != null)
        //                item_motion.dataGridView1.Rows[i].Cells["CBillExtra"].Value = system.set_PricePoint(DR[i]["EXTRA_VALUE"].ToString());

        //            if (DR[i]["EXTRA_RATE"] != null)
        //                item_motion.dataGridView1.Rows[i].Cells["CBillExtraRate"].Value = system.set_PricePoint(DR[i]["EXTRA_RATE"].ToString());


        //            if (DR[i]["DISC_VALUE"] != null)
        //                item_motion.dataGridView1.Rows[i].Cells["CBillDisc"].Value = system.set_PricePoint(DR[i]["DISC_VALUE"].ToString());

        //            if (DR[i]["DISC_RATE"] != null)
        //                item_motion.dataGridView1.Rows[i].Cells["CBillDiscRate"].Value = system.set_PricePoint(DR[i]["DISC_RATE"].ToString());

        //        }
        //        //---------------------bill id
        //        if (DR[i]["BILL_ID"] != null)
        //            item_motion.Bill_id.Add(DR[i]["BILL_ID"]);
        //        else
        //            item_motion.Bill_id.Add("");

        //        //----------------------bill type
        //        if (DR[i]["BILL_TYPE"] != null)
        //            item_motion.Bill_type.Add(DR[i]["BILL_TYPE"]);
        //        else
        //            item_motion.Bill_type.Add("");

        //    }








        //}
        //catch (Exception ex)
        //{
        //}
        //item_motion.DGVRPT.Rows.Add(1);
        //item_motion.DGVRPT.Rows[item_motion.DGVRPT.Rows.Count - 1].Cells["CBillNumber"].Value = "الأجمالي";
        //item_motion.DGVRPT.Rows[item_motion.DGVRPT.Rows.Count - 1].Cells["CIBillQty"].Value = CIBillQty;
        //item_motion.DGVRPT.Rows[item_motion.DGVRPT.Rows.Count - 1].Cells["COBillQty"].Value = COBillQty;
        //item_motion.DGVRPT.Rows[item_motion.DGVRPT.Rows.Count - 1].Cells["CTBillQty"].Value = CIBillQty - COBillQty;

        //item_motion.DGVRPT.Rows[item_motion.DGVRPT.Rows.Count - 1].Cells["CIBillTotal"].Value = system.set_PricePoint(CIBillTotal.ToString());

        //item_motion.DGVRPT.Rows[item_motion.DGVRPT.Rows.Count - 1].Cells["COBillTotal"].Value = system.set_PricePoint(COBillTotal.ToString());

        //item_motion.DGVRPT.Rows[item_motion.DGVRPT.Rows.Count - 1].Cells["CTBillTotal"].Value = system.set_PricePoint(Convert.ToString(CIBillTotal - COBillTotal));

        //item_motion.dataGridView1.Columns["CBillUnit"].Visible = false;

        //if (item_motion.DGVRPT.Rows.Count >= 2)
        //{
        //    if (item_motion.DGVRPT.Rows[item_motion.DGVRPT.Rows.Count - 2].Cells["CTBillPrice"].Value != null)
        //    {
        //        item_motion.LBMovingWaitedAverage.Text = Convert.ToString(item_motion.DGVRPT.Rows[item_motion.DGVRPT.Rows.Count - 2].Cells["CTBillPrice"].Value.ToString());
        //    }
        //}

        //    return false;
        //}
    }
}
