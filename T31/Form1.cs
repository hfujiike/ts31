using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;


namespace T31
{
    public partial class Form1 : Form
    {

        // DLLのインポート
        [System.Runtime.InteropServices.DllImport("user32.dll",
                CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        // 実行中のﾌﾟﾛｸﾞﾗﾑを調べる
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // このプログラムのファイルバージョン
        static System.Diagnostics.FileVersionInfo oFVI =
            System.Diagnostics.FileVersionInfo.GetVersionInfo(
            System.Reflection.Assembly.GetExecutingAssembly().Location);

        static string s_pver = oFVI.FileVersion;  

        // 設定条件データの dictionary の作成
        static Dictionary<string, string> D_jo = new Dictionary<string, string>()
        {
            {"pno","FGK31ES"},
            {"make","FGK-SYSTEMS"},
        };

        // 分割アレイ作成
        static ArrayList al_bcode = new ArrayList();    // 分割コード
        static ArrayList al_browtop = new ArrayList();  // エクセル行前削除カラ
        static ArrayList al_browend = new ArrayList();  // エクセル行前削除マデ
        static int i_bcodearray = 0;                    // こｎアレイの数
        
        // メッセージ関係
        static string s_ermes;
        static string s_scmes;

        // 実行ホルダ
        static string s_apathfull;             // 実行fullﾊﾟｽ
        static string s_apath;                 // 実行ﾊﾟｽ

        // その他
        static string s_jfile;                 // 条件ファイル
        static int xr_headend;                 // エクセル行　ヘッダーエンド
        static int xr_datatop;                 // エクセル行　データトップ
        static int xr_dataend;                 // エクセル行　データエンド
        static int xr_sheetend;                // エクセル行シートの中の最終行
        static int xc_sheetend;                // エクセル行シートの中の最終列

        // -------------------------

        public string a00_ini()
        {
            // ==== 初期化

            string s_rv = "";

            s_apathfull = System.Reflection.Assembly.GetExecutingAssembly().Location;
            s_apath = Path.GetDirectoryName(s_apathfull);

            s_jfile = s_apath + @"\T31jouken.txt";

            label_pv.Text = s_pver;

            //ｴｸｾﾙが終了しているかを確認
            IntPtr hWnd = FindWindow("XLMAIN", null);
            if (hWnd.ToString() != "0")
            {
                string s_wmes1 = "関係しているエクセルがあればこの処理を「キャンセル」し、\r\n"
                               + "エクセルを終了してください";
                string s_wmes2 = "関係していない場合は「OK」で続行してください。";
                if (!CBox(s_wmes1, s_wmes2))
                {
                    s_rv = "ERROR";
                }
            }

            return s_rv;
        }


        public string a01_joukenyomikomi()
        {
            // ==== 条件データFを読み、設定条件データの dictionary へ格納
            string s_rv = "";
            
            string s_rec = "";
            string[] a_item;

            try
            {
                if (File.Exists(s_jfile))
                {
                    s_ermes = "a01 01 jouken file open ";
                    s_scmes += "a01 01 jouken file open \r\n";

                    using (FileStream FS_jouken = new FileStream(
                        s_jfile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    using (StreamReader SR_jouken = new StreamReader(FS_jouken))
                    {
                        while (SR_jouken.Peek() >= 0)
                        {
                            s_ermes = "a01 02 jouken file read";

                            s_rec = SR_jouken.ReadLine();
                            s_rec += ",,,,";
                            a_item = s_rec.Split(',');
                            string s_djo_k = a_item[0];
                            D_jo[s_djo_k] = a_item[1];

                            if (s_djo_k == "buncol") TB_buncol.Text = a_item[1];
                            if (s_djo_k == "bunta") TB_bunta.Text = a_item[1];
                            if (s_djo_k == "hogocol")  TB_hogocol.Text = a_item[1];
                            if (s_djo_k == "hogopw") TB_hogopw.Text = a_item[1];
                            if (s_djo_k == "indir") TB_indir.Text = a_item[1];
                            if (s_djo_k == "fname") TB_fname.Text = a_item[1];
                            if (s_djo_k == "outdir") TB_outdir.Text = a_item[1];

                        }
                        SR_jouken.Close();
                    }
                    
                }
                else
                {
                    // ファイルがないとき
                    s_scmes += "a01 91 jouken file nashi";
                }

                TB_mes.Text = s_scmes;
                TB_mes.SelectionStart = TB_mes.Text.Length;
                TB_mes.Focus();
                TB_mes.ScrollToCaret();
                return s_rv;

            }
            catch (Exception ex)
            {                
                MBox(s_ermes, ex.Message);
                s_scmes += s_ermes + ex.Message + "\r\n";
                TB_mes.Text = s_scmes;
                TB_mes.SelectionStart = TB_mes.Text.Length;
                TB_mes.Focus();
                TB_mes.ScrollToCaret();
                s_rv = "ERROR";
                return s_rv;
            }
        }


        public string a02_jcheck()
        {
            // ==== 条件データのチェック

            string s_rv = "";
            string s_work = "";

            try
            {
                s_ermes = "a02 01 jouken chec k";
                s_scmes += "a02 01 jouken check start" + "\r\n";

                s_ermes += "a02 11 jouken 分割列位置　チェック格納 ";
                s_work = TB_buncol.Text;
                if (suucheck(TB_buncol.Text, 1,9))
                {
                    D_jo["buncol"] = s_work.Trim();
                }
                else
                {
                    s_rv += "分割列位置, ";
                }

                s_ermes += "a02 12 jouken 分割列タイトル　チェック格納 ";
                s_work = TB_bunta.Text.Trim();
                if (s_work != "")
                {
                    D_jo["bunta"] = s_work.Trim();
                }
                else
                {
                    s_rv += "分割列タイトル, ";
                }

                s_ermes += "a02 13 jouken 保護まで列　チェック格納 ";
                s_work = TB_hogocol.Text;
                if (suucheck(TB_buncol.Text, 1, 9))
                {
                    D_jo["hogocol"] = s_work.Trim();
                }
                else
                {
                    s_rv += "保護まで列, ";
                }

                s_ermes += "a02 14 jouken 保護PW　チェック格納 ";
                s_work = TB_hogopw.Text;               
                if (s_work != "")
                {
                    D_jo["hogopw"] = s_work.Trim();
                }
                else
                {
                    s_rv += "保護PW, ";
                }

                s_ermes += "a02 21 jouken その他格納 ";
                D_jo["indir"] = TB_indir.Text;
                D_jo["fname"] = TB_fname.Text;                
                D_jo["outdir"] = TB_outdir.Text;

                s_ermes += "a02 31 jouken error disp ";
                if (s_rv != "")
                {
                    MBox("下記画面項目エラー", s_rv);
                    s_scmes = "a02 91 ERROR 画面項目: " + s_rv + "\r\n";
                    s_rv = "ERROR " + s_rv;
                }
                TB_mes.Text = s_scmes;
                TB_mes.SelectionStart = TB_mes.Text.Length;
                TB_mes.Focus();
                TB_mes.ScrollToCaret();
                return s_rv;
            }
            catch (Exception ex)
            {
                MBox(s_ermes, ex.Message);
                s_scmes += s_ermes + ex.Message;
                TB_mes.Text = s_scmes;
                TB_mes.SelectionStart = TB_mes.Text.Length;
                TB_mes.Focus();
                TB_mes.ScrollToCaret();
                s_rv = "ERROR ";
                return s_rv;
            }

        }        


        public string a03_joukenkakidashi()
        {
            // ==== 条件データFへ dictionary の設定条件データを書き出し

            string s_rv = "";
            string s_rec;

            try
            {
                s_ermes = "a03 01 joukenkakidashi ";
                s_scmes += "\r\n";
                s_scmes += "a03 01 joukenkakidashi start" + "\r\n";

                using (FileStream FS_jw = new FileStream(s_jfile, FileMode.Create))
                using (StreamWriter SW_jw = new StreamWriter(FS_jw))
                {
                    foreach (var px in D_jo)
                    {
                        s_rec = px.Key + "," + px.Value + ",";
                        SW_jw.WriteLine(s_rec);

                    }
                    SW_jw.Close();

                }

                TB_mes.Text = s_scmes;
                TB_mes.SelectionStart = TB_mes.Text.Length;
                TB_mes.Focus();
                TB_mes.ScrollToCaret();
                return s_rv;
            }
            catch (Exception ex)
            {
                MBox(s_ermes, ex.Message);
                s_scmes += s_ermes + ex.Message;
                TB_mes.Text = s_scmes;
                TB_mes.SelectionStart = TB_mes.Text.Length;
                TB_mes.Focus();
                TB_mes.ScrollToCaret();
                s_rv = "ERROR ";
                return s_rv;
            }
        }


        public string a11_excelin()
        {
            // ==== インプットのエクセルファイルを調査とソート

            string s_rv = "";
            int xcol;

            Excel.Application oX = null;
            Excel.Workbook oXB = null;
            Excel.Worksheet oXS = null;
            Excel.Range oXR_sort = null;

            try
            {
                s_scmes += "a11 01 excelin start" + "\r\n";

                s_ermes = "a11 01 excelin APオブジェクトを作成 ";
                oX = new Excel.Application();
                oX.Visible = false;
                oX.Application.DisplayAlerts = false;

                s_ermes = "a11 02 excelin ブックオブジェクトを作成 ";
                string s_exb_in = D_jo["indir"] + @"\" + D_jo["fname"];
                oXB = oX.Workbooks.Open(s_exb_in);

                s_ermes = "a11 03 excelin シートオブジェクトを作成 ";
                oXS = oXB.Sheets[1];

                // 調査
                xr_sheetend = oXS.Range["A1"].SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Row;
                s_scmes += "a11 04 sheetend row: " + xr_sheetend.ToString() + "\r\n";
                xc_sheetend = oXS.Range["A1"].SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Column;
                s_scmes += "a11 05 sheetend col: " + xc_sheetend.ToString() + "\r\n";

                // 分割列
                s_ermes = "a11 06 excelin 分割列 ";
                xcol = int.Parse(D_jo["buncol"]);
                int xrow;
                string s_bun_t = D_jo["bunta"];

                s_scmes += "a11 07 excelin ヘッダーの終わり調査 " + "\r\n";
                s_scmes += "a11 07 excelin xcol: " + xcol.ToString() + "\r\n";
                s_scmes += "a11 07 excelin s_bun_t: " + s_bun_t + "\r\n";


                s_ermes = "a11 09 excelin ヘッダー ";
                for (xrow = 1; xrow < 111; xrow++)
                {
                    if (oXS.Cells[xrow, xcol].Value != null)
                    {
                        if (oXS.Cells[xrow, xcol].Value.GetType() == typeof(System.String))
                        {
                            string s_ebun = oXS.Cells[xrow, xcol].value;
                            if (s_ebun == s_bun_t)
                            {
                                // ヘッダーの終わり（タイトル行）
                                xr_headend = xrow;
                                break;
                            }
                        }
                    }

                    if (xrow > 99)
                    {
                        // ヘッダーの終わり（タイトル行）がない
                        s_scmes += "a11 11 タイトル行不照合\r\n";
                        s_rv = "ERROR ";
                        break;
                    }
                }

                s_scmes += "a11 12 excelin xrow: " + xrow.ToString() + "\r\n";
                s_scmes += "a11 12 excelin xr_headend: " + xr_headend.ToString() + "\r\n";

                if (s_rv == "")
                {
                    s_ermes = "a11 13 excelin データ部分の endline調査 ";
                    xr_datatop = xr_headend + 1;
                    xr_dataend = xr_sheetend;
                    string s_buncode;
                    s_scmes += "a11 13 excelin xr_datatop: " + xr_datatop.ToString() + "\r\n";
                    s_scmes += "a11 13 excelin xr_dataend: " + xr_dataend.ToString() + "\r\n";
                    for (xrow = xr_datatop; xrow <= xr_sheetend; xrow++)
                    {
                        s_ermes = "a11 14 excelin データ部分のデータ部分の終わり xrow= " + xrow.ToString();
                        if (oXS.Cells[xrow, xcol].value == null)
                        {
                            // データ部分の終わり
                            xr_dataend = xrow - 1;
                            break;
                        }
                        try
                        {
                            // 数値なら文字列に変換
                            var v_buncode = oXS.Cells[xrow, xcol].Value;
                            string s_chon = "'";
                            if (v_buncode.GetType() != typeof(System.String))
                            {
                                // 文字列でない
                                s_ermes = "a11 15 excelin 分割コードを文字列に変換 ";
                                double d_buncode = v_buncode;
                                s_buncode = s_chon + d_buncode.ToString();
                                oXS.Cells[xrow, xcol].Value = s_buncode;
                            }

                        }
                        catch (Exception ex)
                        {
                            s_scmes += s_ermes + ex.Message + "\r\n";
                            TB_mes.Text = s_scmes;
                            TB_mes.SelectionStart = TB_mes.Text.Length;
                            TB_mes.Focus();
                            TB_mes.ScrollToCaret();
                            s_rv = "ERROR ";
                            throw new Exception(s_ermes);
                        }
                    }
                }

                if (s_rv == "")
                {
                    try
                    {
                        s_ermes = "a11 16 excelin データ部分ソート ";
                        string s_ce1 = oXS.Cells[xr_datatop, 1].Address;
                        string s_ce2 = oXS.Cells[xr_dataend, xc_sheetend].Address;
                        oXR_sort = oXS.get_Range(s_ce1, s_ce2);
                        s_scmes += "a11 16 excelin ソート " + s_ce1 + "," + s_ce2 + "\r\n";

                        oXR_sort.Sort(
                            oXS.Cells[xr_datatop, xcol],
                            Excel.XlSortOrder.xlAscending);
                        if (oXR_sort != null && Marshal.IsComObject(oXR_sort)) Marshal.ReleaseComObject(oXR_sort);
                    }
                    catch (Exception ex)
                    {
                        s_scmes += s_ermes + ex.Message + "\r\n";
                        TB_mes.Text = s_scmes;
                        TB_mes.SelectionStart = TB_mes.Text.Length;
                        TB_mes.Focus();
                        TB_mes.ScrollToCaret();
                        s_rv = "ERROR ";
                        throw new Exception(s_ermes);
                    }
                }


                if (s_rv == "")
                {
                    s_ermes = "a11 17 excelin 分割アレイ作成 ";

                    // データ行の最初
                    string s_ima;
                    string s_mae;
                    s_ima = oXS.Cells[xr_datatop, xcol].Value;
                    al_bcode.Add(s_ima);
                    al_browtop.Add(xr_datatop);
                    // データ行の2行目以降
                    for (xrow = xr_datatop + 1; xrow < xr_dataend; xrow++)
                    {
                        s_mae = s_ima;
                        s_ima = oXS.Cells[xrow, xcol].Value;
                        if (s_ima != s_mae)
                        {
                            // ブレイクした
                            al_browend.Add(xrow - 1);
                            al_bcode.Add(s_ima);
                            al_browtop.Add(xrow);
                            i_bcodearray++;
                        }
                        else
                        {
                            // 同じだ なにもしない                            
                        }
                    }

                    // データ行の最後
                    al_browend.Add(xr_dataend);
                }


                if (s_rv == "")
                {
                    s_ermes = "a11 18 excelin save ";
                    oXB.Save();
                    oXB.Close(Type.Missing, Type.Missing, Type.Missing);
                }

                if (s_rv == "")
                {
                    s_ermes = "a11 81 excelin end ";
                }

                s_scmes += s_ermes + s_rv + "\r\n";
                TB_mes.Text = s_scmes;
                TB_mes.SelectionStart = TB_mes.Text.Length;
                TB_mes.Focus();
                TB_mes.ScrollToCaret();
                if (oXS != null && Marshal.IsComObject(oXS)) Marshal.ReleaseComObject(oXS);
                if (oXB != null && Marshal.IsComObject(oXB)) Marshal.ReleaseComObject(oXB);
                if (oX != null && Marshal.IsComObject(oX)) oX.Quit();
                if (oX != null && Marshal.IsComObject(oX)) Marshal.ReleaseComObject(oX);
                return s_rv;
            }
            catch (Exception ex)
            {
                MBox(s_ermes, ex.Message);
                s_scmes += s_ermes + ex.Message + "\r\n";
                TB_mes.Text = s_scmes;
                TB_mes.SelectionStart = TB_mes.Text.Length;
                TB_mes.Focus();
                TB_mes.ScrollToCaret();
                if (oXS != null && Marshal.IsComObject(oXS)) Marshal.ReleaseComObject(oXS);
                if (oXB != null && Marshal.IsComObject(oXB)) Marshal.ReleaseComObject(oXB);
                if (oX != null && Marshal.IsComObject(oX)) oX.Quit();
                if (oX != null && Marshal.IsComObject(oX)) Marshal.ReleaseComObject(oX);
                s_rv = "ERROR ";
                return s_rv;
            }
        } 


        public string a12_excelcopy()
        {
            // ==== アウトプットのエクセルファイル作成

            string s_rv = "";
            string s_bcode;
            string s_ifile;
            string s_bfile;
            int i_browtop;
            int i_browend;


            //
            try
            {
                s_scmes += "a12 01 excel out start \r\n";
                s_ermes = "a12 01 excel out start 1   ";

                for ( int i = 0; i <= i_bcodearray; i++)
                {
                    s_ermes = "a12 01 excel out start 2  ( " + i.ToString() + "/" + i_bcodearray.ToString() + ")" ;

                    s_bcode = (String)al_bcode[i];
                    i_browtop = (int)al_browtop[i];
                    i_browend = (int)al_browend[i];

                    s_ermes = "a12 01 excel out start 3   ";

                    s_ifile = D_jo["indir"] + @"\" + TB_fname.Text;
                    s_bfile = D_jo["outdir"] + @"\" + s_bcode + "_" + TB_fname.Text;

                    if (File.Exists(s_bfile))
                    {
                        File.Delete(s_bfile);
                    }

                    s_ermes = "a12 02 excel out copy in_to_out ";
                    File.Copy(s_ifile, s_bfile);

                    s_ermes = "a12 03 excel out call a13_excel_del ";

                    s_rv = a13_exldel(s_bfile, 
                        i_browtop, i_browend,
                        xr_datatop, xr_dataend,
                        xc_sheetend,
                        D_jo["hogocol"], D_jo["hogopw"]);
                }

                s_ermes = "a12 91 excel out end ";
                s_scmes += s_ermes + s_rv + "\r\n";
                TB_mes.Text = s_scmes;
                TB_mes.SelectionStart = TB_mes.Text.Length;
                TB_mes.Focus();
                TB_mes.ScrollToCaret();
                return s_rv;
            }
            catch (Exception ex)
            {
                MBox(s_ermes, ex.Message);
                s_scmes += s_ermes + ex.Message + "\r\n";
                TB_mes.Text = s_scmes;
                TB_mes.SelectionStart = TB_mes.Text.Length;
                TB_mes.Focus();
                TB_mes.ScrollToCaret();
                s_rv = "ERROR ";
                return s_rv;
            }

        }



        public string a13_exldel(string xF, int Brs, int Bre, int Drs, int Dre, int Sce, string hC, string hPW)
        {
            // ==== アウトプットのエクセルファイル不要行削除

            string s_rv = "";

            Excel.Application oX = null;
            Excel.Workbook oXB = null;
            Excel.Worksheet oXS = null;
            Excel.Range oXR_pro = null;

            try
            {
                s_scmes += "a13 01 ex bunkatsu: " + xF + "\r\n";

                s_ermes = "a13 01 ex fuyou del APオブジェクトを作成 ";

                oX = new Excel.Application();
                oX.Visible = false;
                oX.Application.DisplayAlerts = false;

                s_ermes = "a13 02 ex fuyou del ブックオブジェクトを作成 ";
                oXB = oX.Workbooks.Open(xF);

                s_ermes = "a13 03 ex fuyou del シートオブジェクトを作成 ";
                oXS = oXB.Sheets[1];

                string s_delrow;
                
                if (Dre > Bre)
                {
                    // データ行エンド > 分割部分行エンド

                    s_delrow = (Bre + 1).ToString() + ":" + Dre.ToString();
                    oXS.Range[s_delrow].Delete(Type.Missing);

                    s_scmes += "a13 11 ex fuyou del R: " + s_delrow + "\r\n";
                }

                if (Drs < Brs)
                {
                    // データ行スタート < 分割部分行スタート

                    s_delrow = Drs.ToString() + ":" + (Brs - 1).ToString();
                    oXS.Range[s_delrow].Delete(Type.Missing);

                    s_scmes += "a13 12 ex fuyou del F: "+ s_delrow + "\r\n";                    

                }

                // プロテクトを調べる
                if (hC != "" && hPW != "")
                {
                    // プロテクトの場合
                    //  シートプロテクトしない編集可能範囲を設定
                    int i_hogocol = int.Parse(hC);
                    string s_hogopw = hPW;
                    string s_cps = oXS.Cells[1, i_hogocol].Address;
                    string s_cpe = oXS.Cells[Dre, Sce].Address;
                    oXR_pro = oXS.get_Range(s_cps, s_cpe);                    
                    oXS.Protection.AllowEditRanges.Add(
                        "data_prodect",
                        oXR_pro,
                        Type.Missing);
                    //  シートプロテクトをかける
                    oXS.Protect(
                        s_hogopw, Type.Missing, Type.Missing, Type.Missing,
                       Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                       Type.Missing, true, Type.Missing, Type.Missing,
                       Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
                
                // 格納のセーブ
                oXB.Save();
                oXB.Close();
                if (oXR_pro != null && Marshal.IsComObject(oXR_pro)) Marshal.ReleaseComObject(oXR_pro);
                if (oXS != null && Marshal.IsComObject(oXS)) Marshal.ReleaseComObject(oXS);
                if (oXB != null && Marshal.IsComObject(oXB)) Marshal.ReleaseComObject(oXB);
                if (oX != null && Marshal.IsComObject(oX)) oX.Quit();
                if (oX != null && Marshal.IsComObject(oX)) Marshal.ReleaseComObject(oX);
                
                TB_mes.Text = s_scmes;
                TB_mes.SelectionStart = TB_mes.Text.Length;
                TB_mes.Focus();
                TB_mes.ScrollToCaret();
                return s_rv;
            }
            catch (Exception ex)
            {
                MBox(s_ermes, ex.Message);
                s_scmes += s_ermes + ex.Message + "\r\n";
                TB_mes.Text = s_scmes;
                TB_mes.SelectionStart = TB_mes.Text.Length;
                TB_mes.Focus();
                TB_mes.ScrollToCaret();
                s_rv = "ERROR ";
                if (oXR_pro != null && Marshal.IsComObject(oXR_pro)) Marshal.ReleaseComObject(oXR_pro);
                if (oXS != null && Marshal.IsComObject(oXS)) Marshal.ReleaseComObject(oXS);
                if (oXB != null && Marshal.IsComObject(oXB)) Marshal.ReleaseComObject(oXB);
                if (oX != null && Marshal.IsComObject(oX)) oX.Quit();
                if (oX != null && Marshal.IsComObject(oX)) Marshal.ReleaseComObject(oX);

                return s_rv;
            }
        }


        public string a91_end()
        {
            // ==== 最終処理

            string s_rv = "";
            //
            s_scmes += "a91 91 正常終了 " + "\r\n";

            TB_mes.Text = s_scmes;
            TB_mes.SelectionStart = TB_mes.Text.Length;
            TB_mes.Focus();
            TB_mes.ScrollToCaret();
            return s_rv;
        }


        static string MBox(string e1, string e2)
        {
            // ==== メッセージボックス

            MessageBox.Show(e1 + "\r\n" + e2,
                        "Excel分割処理ERROR",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
            return "";
        }


        static bool CBox(string e1, string e2)
        {
            //  ==== キャンセルボックス

            DialogResult dr = MessageBox.Show(
                    e1 + "\r\n" + e2,
                        "Excel分割処理Warning",
                    MessageBoxButtons.OKCancel,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2);
            if (dr == System.Windows.Forms.DialogResult.Cancel)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public Boolean suucheck(string s1, int n1, int n2)
        {
            // ==== 数値の範囲内チェック

            Boolean b_rv = false;
            string s_work = "";
            Double d_work = 0;
            s_work = s1.Trim();

            if (double.TryParse(s_work  , out d_work))
            {
                if (d_work >= n1)
                {
                    if (d_work <= n2)
                    {
                        b_rv = true;
                    }
                }
            }

            return b_rv;
        } 


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string s_a = "";

            s_a = a00_ini();                           // 起動　ini
            if (s_a == "ERROR") Application.Exit();

            s_a = a01_joukenyomikomi();                // 起動　条件ファイル読み込み
            if(s_a == "ERROR") Application.Exit();
        }

        private void button_in_MouseClick(object sender, MouseEventArgs e)
        {
            string s_pfile = "";

            OpenFileDialog oFD = new OpenFileDialog();
            oFD.InitialDirectory = @"c:\";
            oFD.Title = "入力ファイル設定";

            if (oFD.ShowDialog() == DialogResult.OK)
            {
                s_pfile = oFD.FileName;
            }
            oFD.Dispose();

            TB_indir.Text = Path.GetDirectoryName(s_pfile);
            TB_fname.Text = Path.GetFileName(s_pfile);

        }

        private void button_out_MouseClick(object sender, MouseEventArgs e)
        {
            FolderBrowserDialog oFolderBD1 = new FolderBrowserDialog();
            oFolderBD1.Description = "出力ﾌｫﾙﾀﾞ設定";
            oFolderBD1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            oFolderBD1.SelectedPath = TB_indir.Text;
            if (oFolderBD1.ShowDialog() == DialogResult.OK)
            {
                TB_outdir.Text = oFolderBD1.SelectedPath;
            }
            oFolderBD1.Dispose();
        }

        private void button_run_MouseClick(object sender, MouseEventArgs e)
        {
            string s_a = "";

            s_a = a02_jcheck();                        // 起動　条件チェック

            if (s_a == "")
            {
                s_a = a03_joukenkakidashi();           // 起動　条件書き出し
            }
            if (s_a == "")
            {
                s_a = a11_excelin();                   // 起動　エクセル読み込み
            }
            if (s_a == "")
            {
                s_a = a12_excelcopy();                 // 起動　エクセル分割コピー　と　不要行削除とセーブ
            }
            if (s_a == "")
            {
                s_a = a91_end();                       // 起動　終了処理
            }

        }

    }

    // ---------------------------------------------------------------------------
    // ---- fgk 20170701 -
    // ---- fgk 20170708 -

}
