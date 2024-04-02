using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Hospital
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Random random = new Random();

        int month;
        

        int patient_admission_rate, //скорость поступления пациентов
            patient_reduction_speed,//скорость ухода пациентов
            patient_num = 20,//кол-во больных
            hospital_workload = 50,//загруженность больницы
            hospital_expansion = 1,//расширение больницы
            num_of_employees= 30,//кол-во персонала
            duration_of_treatment = 7,//продолжительность лечения
            duration_of_the_examination,//продолжительность обследования
            new_requests = 2; //новые обращения

        double patient_admission_rate_coef = 0.5 , //скорость поступления пациентов
             patient_reduction_speed_coef = 0.5,//скорость ухода пациентов
             patient_num_coef = 1,//кол-во больных
             hospital_workload_coef = 0.8,//загруженность больницы
             hospital_expansion_coef = 0.7,//расширение больницы
             num_of_employees_coef = 0.8,//кол-во персонала
             duration_of_treatment_coef = 0.7,//продолжительность лечения
             duration_of_the_examination_coef = 0.6,//продолжительность обследования
             new_requests_coef = 0.6; //новые обращения  


        private void btStop_Click(object sender, EventArgs e)
        {
            Timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {

            month++;



            patient_num += (int)(patient_admission_rate_coef * patient_admission_rate);
           
            hospital_expansion = random.Next(0, 100);
            num_of_employees += (int)(hospital_expansion_coef*hospital_expansion);
            hospital_workload += (int)(-num_of_employees_coef * num_of_employees - hospital_expansion_coef * hospital_expansion + patient_num_coef * patient_num);

            duration_of_treatment += (int)(hospital_workload_coef * hospital_workload + duration_of_the_examination_coef * duration_of_the_examination);

            patient_reduction_speed += (int)(-duration_of_treatment_coef * duration_of_treatment );

            new_requests += (int)(-duration_of_treatment_coef * duration_of_treatment);

            patient_admission_rate += (int)(new_requests_coef * new_requests);
           
            
            chart1.Series[0].Points.AddXY(month, patient_admission_rate);
            chart1.Series[1].Points.AddXY(month, patient_reduction_speed);

        }

        private void btSimulate_Click(object sender, EventArgs e)
        {
            month = 0;

            duration_of_the_examination = Convert.ToInt32(textBox1.Text);
            patient_admission_rate = Convert.ToInt32(textBox2.Text); 
            patient_reduction_speed = Convert.ToInt32(textBox3.Text);

            //custom = random.Next(10000, 40000); //кол-во заказов
            //industry = random.Next(50000, 100000); //кол-во груза

            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();          
           
            chart1.Series[0].Points.AddXY(month, patient_admission_rate);
            chart1.Series[1].Points.AddXY(month, patient_reduction_speed);

            if (!Timer.Enabled)
            {
                Timer.Start();
            }
        }
    }
}
