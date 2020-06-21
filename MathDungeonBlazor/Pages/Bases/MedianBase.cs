using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathDungeonBlazor.Pages.Bases
{
    public class MedianBase : ComponentBase
    {
        //Simple median program

        //Declaring string obj
        public class Output_String_Median_1
        {
            public string Variable { get; set; }

            public Output_String_Median_1(string variable)
            {
                Variable = variable;
            }
        }

        //Declaring Median variables

        //Declating input median number
        protected double median_number;
        //Declaring List for storing numbers for median (unsorted list)
        protected List<double> median_numbers_unsorted = new List<double>();
        //Declaring List for storing numbers for median (sorted list)
        protected List<double> median_numbers_sorted = new List<double>();
        //Declaring List for output string for unsorted list in razor page
        protected List<Output_String_Median_1> output_string_unsorted = new List<Output_String_Median_1>();
        //Declaring List for output string for sorted list in razor page
        protected List<Output_String_Median_1> output_string_sorted = new List<Output_String_Median_1>();
        //Declaring variable for result
        protected double simple_median_result;
        //This variable will show message if median is calculated as even or not
        protected string isEven = "";

        /// <param name="Add_Number_1">Adds number to the median_numbers_unsorted</param>
        public async void Add_Number_1()
        {
            if (median_number != null)
            {
                string output_partial = "";

                if (median_numbers_unsorted.Count > 0)
                {
                    output_partial = output_partial + " , " + median_number.ToString();
                    Output_String_Median_1 obj = new Output_String_Median_1(output_partial);
                    output_string_unsorted.Add(obj);
                }
                else
                {
                    output_partial = output_partial + median_number.ToString();
                    Output_String_Median_1 obj = new Output_String_Median_1(output_partial);
                    output_string_unsorted.Add(obj);
                }

                median_numbers_unsorted.Add(median_number);
                median_number = 0;
                StateHasChanged();
            }
        }

        /// <param name="Calc_Median_Simple">
        /// Sorts median_numbers_unsorted and adds them to median_numbers_sorted,
        /// then calculates Median 
        /// </param>
        public async void Calc_Median_Simple()
        {
            //Array will be sorted by use of bubble sort algorithm
            int list_Lenght = median_numbers_unsorted.Count;
            List<double> tempList = median_numbers_unsorted;

            for (int i = 0; i < list_Lenght - 1; i++)
                for (int j = 0; j < list_Lenght - i - 1; j++)
                    if (tempList[j] > tempList[j + 1])
                    {
                        double temp = tempList[j];
                        tempList[j] = tempList[j + 1];
                        tempList[j + 1] = temp;
                    }

            //Creating output string
            for(int i = 0; i < tempList.Count; i++)
            {
                if(i != tempList.Count - 1)
                {
                    Output_String_Median_1 t = new Output_String_Median_1(tempList[i].ToString() + ",");
                    output_string_sorted.Add(t);
                }
                else
                {
                    Output_String_Median_1 t = new Output_String_Median_1(tempList[i].ToString());
                    output_string_sorted.Add(t);
                }
            }
            median_numbers_sorted = tempList;
            
            //Finding Median
            if((list_Lenght)%2 == 0)
            {
                //If ammount of numbers is even, then get 2 middle numbers and calculate mean for median
                double t1 = median_numbers_sorted[(list_Lenght / 2) - 1];
                double t2 = median_numbers_sorted[(list_Lenght / 2)];

                simple_median_result = (t1 + t2) / 2;
                isEven = "even data table.";
                StateHasChanged();
            }
            else
            {
                //If ammount of numbers is not even, choose middle number for median
                simple_median_result = median_numbers_sorted[list_Lenght / 2];
                isEven = "odd data table.";
                StateHasChanged();

            }
        }

        /// <param name="Delete_Simple_Median_Var">This function will delete last number from the list</param>
        public async void Delete_Simple_Median_Var()
        {
            if(median_numbers_unsorted.Count != 0)
            {
                median_numbers_unsorted.RemoveAt(median_numbers_unsorted.Count - 1);
                output_string_unsorted.RemoveAt(output_string_unsorted.Count - 1);
                output_string_sorted.Clear();
                simple_median_result = 0;
                StateHasChanged();
            }
        }

        /// <param name="Reset_Simple_Median">This function will reset simple median program</param>
        public async void Reset_Simple_Median()
        {
            median_numbers_sorted.Clear();
            median_numbers_unsorted.Clear();
            output_string_unsorted.Clear();
            output_string_sorted.Clear();
            simple_median_result = 0;
            StateHasChanged();
        }

        /////////////////////////////////////////////////////////////////
        ///

        //Median for a Simple Frequency Distribution Program

        /// <param name="SFDDC_Object">Simple Frequency Distribution Data Class declaration</param>
        public class SFDDC_Object
        {
            public int Id { get; set; }
            public double X { get; set; }
            public double F { get; set; }
            public double Cumf { get; set; }

            public SFDDC_Object(double x, double f)
            {
                X = x;
                F = f;
            }
        }

        //Declaring Median variables for a Simple Frequency Distribution Razor Page

        //Declaring List for SFDDC_Object's
        protected List<SFDDC_Object> simple_frequency_data_median = new List<SFDDC_Object>();
        //Checking if random generated data is used
        protected bool isRandom_SFD = false;
        //Checking if custom data is used
        protected bool isCustom_SFD = false;

        //Text for button which generates mock data for simple_frequency_data_median
        protected string btn_s_f_text = "Generate Random Data";
        //Variables for custom input
        protected double x_SFD;
        protected double f_SFD;
        //Result form ((Cumf[last object] + 1) / 2)
        protected double centralItemIdentyfier;
        //Result variable
        protected double result_SFD = 0;
        protected double result_x_SFD = 0;


        /// <param name="Add_C_SFDO">This function will add custom variable to List simple_frequency_data_median </param>
        public async void Add_C_SFDO()
        {
            isCustom_SFD = true;
            if(simple_frequency_data_median.Count == 0)
            {
                SFDDC_Object obj = new SFDDC_Object(x_SFD, f_SFD);
                obj.Cumf = obj.F;
                obj.Id = simple_frequency_data_median.Count;
                simple_frequency_data_median.Add(obj);
                x_SFD = 0;
                f_SFD = 0;
            }
            else
            {
                int lenght = simple_frequency_data_median.Count;
                SFDDC_Object obj = new SFDDC_Object(x_SFD, f_SFD);
                obj.Cumf = obj.F + simple_frequency_data_median[lenght - 1].Cumf;
                obj.Id = simple_frequency_data_median.Count;
                simple_frequency_data_median.Add(obj);
                x_SFD = 0;
                f_SFD = 0;
            }
            centralItemIdentyfier = 0;
            result_x_SFD = 0;
            result_SFD = 0;
            StateHasChanged();

        }

        /// <param name="Delete_C_SFDO">This function will delete last object in List simple_frequency_data_median</param>
        public async void Delete_C_SFDO()
        {
            if(simple_frequency_data_median.Count != 0)
            {
                int lenght = simple_frequency_data_median.Count;
                simple_frequency_data_median.RemoveAt(lenght - 1);

                //Reset Id numbers
                for (int i = 0; i < simple_frequency_data_median.Count; i++)
                {
                    simple_frequency_data_median[i].Id = i;
                }
            }
            if(simple_frequency_data_median.Count == 0)
            {
                isRandom_SFD = false;
                isCustom_SFD = false;
            }
            centralItemIdentyfier = 0;
            result_x_SFD = 0;
            result_SFD = 0;
        }

        /// <param name="Delete_SFDO_ByID">This function will delete last simple_frequency_data_object from custom list simple_frequency_data_median by its Id number</param>
        public async void Delete_SFDO_ByID(int id)
        {
            if (simple_frequency_data_median.Count != 0)
            {
                simple_frequency_data_median.RemoveAt(id);

                //Reset Id numbers
                for (int i = 0; i < simple_frequency_data_median.Count; i++)
                {
                    simple_frequency_data_median[i].Id = i;
                }

                if (simple_frequency_data_median.Count == 0)
                {
                    isRandom_SFD = false;
                    isCustom_SFD = false;

                    btn_s_f_text = "Generate Random Data";
                }

                centralItemIdentyfier = 0;
                result_x_SFD = 0;
                result_SFD = 0;
            }
        }

        /// <param name="RESET_SFD">This function will reset Simple Frequency Distribution for Median program</param>
        public async void RESET_SFD()
        {
            btn_s_f_text = "Generate Random Data";
            result_SFD = 0;
            isRandom_SFD = false;
            isCustom_SFD = false;
            centralItemIdentyfier = 0;
            result_x_SFD = 0;
            result_SFD = 0;
            simple_frequency_data_median.Clear();
        }
        /// <param name="Generate_Simple_Freq_D">This function will generate mock data for simple_frequency_data_median List</param>
        public async void Generate_Simple_Freq_D()
        {
            simple_frequency_data_median.Clear();
            for(int i = 0; i <= 10; i++)
            {
                if(simple_frequency_data_median.Count == 0)
                {
                    SFDDC_Object obj = new SFDDC_Object(0, RandomNumber(1 * 2, 1 * 100));
                    obj.Cumf = obj.F;
                    obj.Id = i;
                    simple_frequency_data_median.Add(obj);
                }
                else
                {
                    SFDDC_Object obj = new SFDDC_Object(i, RandomNumber(1 * 2, 1 * 100));
                    obj.Cumf = simple_frequency_data_median[i - 1].Cumf + obj.F;
                    obj.Id = i;
                    simple_frequency_data_median.Add(obj);
                }
            }
            centralItemIdentyfier = 0;
            result_x_SFD = 0;
            result_SFD = 0;
            isRandom_SFD = true;
            btn_s_f_text = "Reset Data";
            StateHasChanged();
        }

        /// <param name="Calculate_SFDM">This fuction will Calcualte_Simple Frequency Distribution Median </param>
        public void Calculate_SFDM()
        {
            if(simple_frequency_data_median.Count > 0)
            {
                centralItemIdentyfier = ((simple_frequency_data_median[simple_frequency_data_median.Count - 1].Cumf + 1) / 2);
                int i = 0;
                while(centralItemIdentyfier > result_SFD)
                {
                    result_SFD = simple_frequency_data_median[i].Cumf;
                    result_x_SFD = simple_frequency_data_median[i].X;
                    i++;
                }
            }
        }


        //This function will generate random number
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }

        ////////////////////////////////////////////////////////////
        ///

        //Median for a grouped frequency distribution


        /// <param name="GFDDC_Object">Grouped Frequency Distribution Data Class declaration</param>
        public class GFDDC_Object
        {
            public int Id { get; set; }
            public double Range1 { get; set; }
            public double Range2 { get; set; }
            public double F { get; set; }
            public double CumF { get; set; }

            public GFDDC_Object()
            {

            }

            public GFDDC_Object(double r1, double r2, double f)
            {
                Range1 = r1;
                Range2 = r2;
                F = f;
            }
        }

        //Declaring Median variables for a Grouped Frequency Distribution Razor Page

        //Declaring List for GFDDC_Object's
        protected List<GFDDC_Object> grouped_frequency_data_median = new List<GFDDC_Object>();
        //Checking if random generated data is used
        protected bool isRandom_GFD = false;
        //Checking if custom data is used
        protected bool isCustom_GFD = false;
        //If to show formula
        protected bool isGDF_Calculated = false;
        //Text for button which generates mock data for grouped_frequency_data_median
        protected string btn_g_f_text = "Generate Random Data";
        //Variables for custom input
        protected double range1;
        protected double range2;
        protected double f_GFD;

        //Function Variables//////////////////////////////
        protected double halfCUMF;
        //Lower bound of the median class
        protected double LM;
        //Actual frequency median class
        protected double fM;
        //Median class width
        protected double cM;
        //Culminative frequency of class immediately prior to the median class
        protected double CF_prior;
        //////////////////////////////////////////////////

        //Result variable
        protected double result_GFD;



        /// <param name="Add_C_GFDO">This function will add custom variable to List grouped_frequency_data_median </param>
        public void Add_C_GFDO()
        {
            if(grouped_frequency_data_median.Count == 0)
            {
                GFDDC_Object obj = new GFDDC_Object(range1, range2, f_GFD);
                obj.CumF = obj.F;
                obj.Id = grouped_frequency_data_median.Count;
                grouped_frequency_data_median.Add(obj);
                range1 = 0;
                range2 = 0;
                f_GFD = 0;
                isCustom_GFD = true;
            }
            else
            {
                int lenght = grouped_frequency_data_median.Count;
                GFDDC_Object temp = grouped_frequency_data_median[lenght - 1];
                double r1 = temp.Range2;
                double r2 = r1 + (temp.Range2 - temp.Range1);

                GFDDC_Object obj = new GFDDC_Object(r1, r2, f_GFD);
                obj.CumF = grouped_frequency_data_median[lenght - 1].CumF + obj.F;
                obj.Id = grouped_frequency_data_median.Count;
                grouped_frequency_data_median.Add(obj);
                range1 = 0;
                range2 = 0;
                f_GFD = 0;
            }

            isGDF_Calculated = false;
            result_GFD = 0;
        }

        /// <param name="Delete_C_GFDO">This function will delete last object in List grouped_frequency_data_median</param>
        public void Delete_C_GFDO()
        {
            if(grouped_frequency_data_median.Count != 0)
            {
                int lenght = grouped_frequency_data_median.Count;
                grouped_frequency_data_median.RemoveAt(lenght - 1);
                result_GFD = 0;

                //Reset Id numbers
                for (int i = 0; i < grouped_frequency_data_median.Count; i++)
                {
                    grouped_frequency_data_median[i].Id = i;
                }
            }

            if(grouped_frequency_data_median.Count == 0)
            {
                isCustom_GFD = false;
                isGDF_Calculated = false;
            }
        }

        /// <param name="Delete_GDFDO_ByID">This function will delete last grouped_frequency_data_median_object from custom list grouped_frequency_data_median by its Id number</param>
        public async void Delete_GDFDO_ByID(int id)
        {
            if (grouped_frequency_data_median.Count != 0)
            {
                grouped_frequency_data_median.RemoveAt(id);

                //Reset Id numbers
                for (int i = 0; i < grouped_frequency_data_median.Count; i++)
                {
                    grouped_frequency_data_median[i].Id = i;
                }

                if (grouped_frequency_data_median.Count == 0)
                {
                    isRandom_GFD = false;
                    isCustom_GFD = false;

                    btn_g_f_text = "Generate Random Data";
                }

                isGDF_Calculated = false;
                result_GFD = 0;
            }
        }

        /// <param name="RESET_GFD">This function will reset Grouped Frequency Distribution for Median program</param>
        public async void RESET_GFD()
        {
            grouped_frequency_data_median.Clear();
            isCustom_GFD = false;
            isRandom_GFD = false;
            result_GFD = 0;
            range1 = 0;
            range2 = 0;
            f_GFD = 0;
            isGDF_Calculated = false;
            btn_g_f_text = "Generate Random Data";
        }

        /// <param name="Generate_Grouped_Freq_D">This function will generate mock data for grouped_frequency_data_median List</param>
        public async void Generate_Grouped_Freq_D()
        {
            grouped_frequency_data_median.Clear();
            isRandom_GFD = true;
            int r1 = 5;
            int r2 = 10;

            for(int i = 0; i <= 10; i++)
            {
                if(grouped_frequency_data_median.Count == 0)
                {
                    double f = RandomNumber(100, 1000);
                    GFDDC_Object obj = new GFDDC_Object(r1, r2, f);
                    obj.CumF = f;
                    grouped_frequency_data_median.Add(obj);
                    r1 += 5;
                    r2 += 5;
                }
                else
                {
                    int lenght = grouped_frequency_data_median.Count;
                    GFDDC_Object temp = grouped_frequency_data_median[lenght - 1];
                    double f = RandomNumber(100, 1000);
                    GFDDC_Object obj = new GFDDC_Object(r1, r2, f);
                    obj.CumF = temp.CumF + obj.F;
                    r1 += 5;
                    r2 += 5;
                    grouped_frequency_data_median.Add(obj);
                }
            }
            btn_g_f_text = "Reset Data";
            StateHasChanged();
        }

        /// <param name="Calculate_GFDM">This fuction will Calcualte_Grouped Frequency Distribution Median </param>
        public void Calculate_GFDM()
        {
            isGDF_Calculated = true;
            int lenght = grouped_frequency_data_median.Count;

            halfCUMF = (grouped_frequency_data_median[lenght - 1].CumF) / 2;

            int i = 0;
            GFDDC_Object temp = grouped_frequency_data_median[i];

            while(halfCUMF > grouped_frequency_data_median[i].CumF)
            {
                temp = grouped_frequency_data_median[i];
                i++;
            }

            CF_prior = temp.CumF;
            fM = temp.F;
            LM = temp.Range1;
            cM = temp.Range2 - temp.Range1;

            result_GFD = LM + (((halfCUMF - CF_prior) / fM) * cM);
        }
    }
}
