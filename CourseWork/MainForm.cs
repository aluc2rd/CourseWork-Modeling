using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CourseWork
{
    public partial class MainForm : Form
    {
      
// Определение приватных полей класса MainForm
        private ushort A { get; set; } // Поле для хранения числа A
        private ushort B { get; set; } // Поле для хранения числа B
        private ControlMachine ControlMachine { get; } // Поле для хранения ссылки на объект ControlMachine
        private MicroProgram MicroProgram { get; set; } // Поле для хранения ссылки на объект MicroProgram

        // Конструктор класса MainForm
        public MainForm()
        {
            // Инициализация компонентов формы
            InitializeComponent();

            // Установка начальных значений текстовых полей
            textbox_NumberA.Text = @"0.00000"; // Установка начального значения текстового поля для числа A
            textbox_NumberB.Text = @"0.00000"; // Установка начального значения текстового поля для числа B
            textBox_NumberC.Text = @"0.00000"; // Установка начального значения текстового поля для числа C

            // Настройка автоматического изменения ширины колонок в таблицах
            dataGridView_number_A.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Отключение автоматического изменения ширины колонок
            dataGridView_number_B.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Отключение автоматического изменения ширины колонок
            dataGridView_number_C.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Отключение автоматического изменения ширины колонок
            dataGridView_register_B.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Отключение автоматического изменения ширины колонок
            dataGridView_count.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None; // Отключение автоматического изменения ширины колонок

            // Изменение шрифта в таблицах
            dataGridView_register_B.Font = new Font("Microsoft Sans Serif", 8); // Установка шрифта для таблицы dataGridView_register_B
            dataGridView_count.Font = new Font("Microsoft Sans Serif", 8); // Установка шрифта для таблицы dataGridView_count

            // Создание колонок в таблице dataGridView_count
            var widthColumn = 25; // Ширина колонки
            var width = 0; // Счетчик ширины
            for (var i = 3 - 1; i >= 0; i--)
            {
                var index = dataGridView_count.Columns.Add("column_" + i, i.ToString()); // Добавление колонки с индексом i
                dataGridView_count.Columns[index].Width = widthColumn; // Установка ширины колонки
                width += widthColumn; // Увеличение счетчика ширины
            }

            // Установка высоты и ширины таблицы dataGridView_count
            dataGridView_count.Height = 45;
            dataGridView_count.Width = width + 3;

            // Создание колонок в таблицах dataGridView_number_A, dataGridView_number_B и dataGridView_register_B
            width = 0; // Счетчик ширины
            for (int i = 16 - 1; i >= 0; i--)
            {
                int index = dataGridView_number_A.Columns.Add("column_" + i, i.ToString()); // Добавление колонки с индексом i в таблицу dataGridView_number_A
                dataGridView_number_B.Columns.Add("column_" + i, i.ToString()); // Добавление колонки с индексом i в таблицу dataGridView_number_B
                dataGridView_register_B.Columns.Add("column_" + i, i.ToString()); // Добавление колонки с индексом i в таблицу dataGridView_register_B
                dataGridView_number_A.Columns[index].Width = widthColumn; // Установка ширины колонки
                dataGridView_number_B.Columns[index].Width = widthColumn; // Установка ширины колонки
                dataGridView_register_B.Columns[index].Width = widthColumn; // Установка ширины колонки
                width += widthColumn; // Увеличение счетчика ширины
            }

        
            // Установка высоты и ширины таблицы dataGridView_number_A
            dataGridView_number_A.Height = 45;
            dataGridView_number_A.Width = width + 2;

            // Установка высоты и ширины таблицы dataGridView_number_B
            dataGridView_number_B.Height = 45;
            dataGridView_number_B.Width = width + 3;

            // Установка высоты и ширины таблицы dataGridView_register_B
            dataGridView_register_B.Height = 45;
            dataGridView_register_B.Width = width + 3;

            // Создание колонок в таблице dataGridView_number_C
            width = 0; // Счетчик ширины
            for (int i = 32 - 1; i >= 0; i--)
            {
                int index = dataGridView_number_C.Columns.Add("column_" + i, i.ToString()); // Добавление колонки с индексом i
                dataGridView_number_C.Columns[index].Width = widthColumn; // Установка ширины колонки
                width += widthColumn; // Увеличение счетчика ширины
            }

            // Установка высоты и ширины таблицы dataGridView_number_C
            dataGridView_number_C.Height = 45;
            dataGridView_number_C.Width = width + 3;

            // Добавление строк в таблицы
            dataGridView_number_A.Rows.Add(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            dataGridView_number_B.Rows.Add(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            dataGridView_register_B.Rows.Add(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            dataGridView_number_C.Rows.Add(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
            dataGridView_count.Rows.Add(0, 0, 0);

            // Установка стиля границ таблиц
            dataGridView_number_A.BorderStyle = BorderStyle.FixedSingle;
            dataGridView_number_B.BorderStyle = BorderStyle.FixedSingle;
            dataGridView_number_C.BorderStyle = BorderStyle.FixedSingle;
            dataGridView_register_B.BorderStyle = BorderStyle.FixedSingle;
            dataGridView_count.BorderStyle = BorderStyle.FixedSingle;

            // Скрытие заголовков строк в таблицах
            dataGridView_number_A.RowHeadersVisible = false;
            dataGridView_number_B.RowHeadersVisible = false;
            dataGridView_number_C.RowHeadersVisible = false;
            dataGridView_count.RowHeadersVisible = false;
            dataGridView_register_B.RowHeadersVisible = false;

            // Установка выравнивания текста в ячейках таблиц
            dataGridView_number_A.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView_number_B.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView_number_C.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView_count.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridView_register_B.RowsDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            // Установка режима масштабирования изображения в PictureBox
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            // Инициализация объектов ControlMachine и MicroProgram
            ControlMachine = new ControlMachine(this);
            MicroProgram = new MicroProgram(this);

            // Установка флажка для radioButton_a0
            radioButton_a0.Checked = true;
        }

        // Обработчик события нажатия на ячейку в таблице dataGridView_number_A
        private void DataGridView_number_A_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Если нажата заголовочная строка таблицы, выходим из функции
            if (e.RowIndex == -1)
                return;

            // Получаем значение ячейки
            var value = (int)dataGridView_number_A.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            // Изменяем значение ячейки на противоположное (0 или 1)
            value = value == 0 ? 1 : 0;
            dataGridView_number_A.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = value;

            // Получаем все ячейки текущей строки
            var cells = dataGridView_number_A.Rows[e.RowIndex].Cells;

            // Конвертируем значения ячеек строки в двоичную строку
            var strB = new StringBuilder();
            for (var i = 0; i < cells.Count; i++)
                strB.Append(cells[i].Value);

            // Очищаем текстовое поле для числа A
            textbox_NumberA.Text = "";

            // Флаг отрицательного числа
            var denial = false;
            ushort a = 0;
            // Если первый бит равен 1, то число отрицательное
            if (strB[0] - '0' == 1)
            {
                // Добавляем знак минус в текстовое поле
                textbox_NumberA.Text = "-";

                // Конвертируем двоичную строку в число с учетом знака
                a = (ushort)Convert.ToInt16(strB.ToString(), 2);

                // Заменяем первый бит на 0
                strB.Replace("1", "0", 0, 1);

                // Устанавливаем флаг отрицательного числа
                denial = true;
            }

            // Конвертируем двоичную строку в число без знака
            A = (ushort)Convert.ToInt16(strB.ToString(), 2);

            // Формируем двоичную строку для отображения числа в текстовом поле
            strB = new StringBuilder().Append("00000");
            for (var i = 0; i < 5; i++)
                if ((int)(A / Math.Pow(10, i)) != 0)
                    strB.Remove(0, 1);

            // Добавляем десятичную точку и двоичную строку в текстовое поле
            textbox_NumberA.Text += "0." + strB + A;

            // Если число отрицательное, устанавливаем его значение
            A = denial ? a : A;
        }

 
        // Обработчик события нажатия на ячейку в таблице dataGridView_number_B
        private void DataGridView_number_B_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Если нажата заголовочная строка таблицы, выходим из функции
            if (e.RowIndex == -1)
                return;

            // Получаем значение ячейки
            var value = (int)dataGridView_number_B.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            // Изменяем значение ячейки на противоположное (0 или 1)
            value = value == 0 ? 1 : 0;

            // Конвертируем значения ячеек строки в двоичную строку
            var strB = CalculationResultNumber(dataGridView_number_B, e.RowIndex, e.ColumnIndex, value);

            // Очищаем текстовое поле для числа B
            textbox_NumberB.Text = "";

            // Флаг отрицательного числа
            var denial = false;
            ushort b = 0;
            // Если первый бит равен 1, то число отрицательное
            if (strB[0] - '0' == 1)
            {
                // Добавляем знак минус в текстовое поле
                textbox_NumberB.Text = "-";

                // Конвертируем двоичную строку в число с учетом знака
                b = (ushort)Convert.ToInt16(strB.ToString(), 2);

                // Заменяем первый бит на 0
                strB.Replace("1", "0", 0, 1);

                // Устанавливаем флаг отрицательного числа
                denial = true;
            }

            // Конвертируем двоичную строку в число без знака
            B = (ushort)Convert.ToInt16(strB.ToString(), 2);

            // Формируем двоичную строку для отображения числа в текстовом поле
            strB = new StringBuilder().Append("00000");
            for (var i = 0; i < 5; i++)
                if ((int)(B / Math.Pow(10, i)) != 0)
                {
                    // Удаление лишних нулей слева
                    strB.Remove(0, 1);
                }

            // Добавляем десятичную точку и двоичную строку в текстовое поле
            textbox_NumberB.Text += "0." + strB + B;

            // Если число отрицательное, устанавливаем его значение
            B = denial ? b : B;
        }


        // Функция для конвертации значений ячеек строки в двоичную строку
        private StringBuilder CalculationResultNumber(DataGridView table, int row, int cell, int value)
        {
            // Установка нового значения ячейки
            table.Rows[row].Cells[cell].Value = value;

            // Получение всех ячеек текущей строки
            var cells = table.Rows[row].Cells;

            // Конвертация значений ячеек в двоичную строку
            var strB = new StringBuilder();
            for (var i = 0; i < cells.Count; i++)
                strB.Append(cells[i].Value);

            // Возврат двоичной строки
            return strB;
        }



        // Метод для обновления информации в регистрах
        public void UpdateInfoRegister(ushort b, byte count, uint c)
        {
            // Обновление регистра B
            UpdateInfoRegister(dataGridView_register_B, b, 16);

            // Обновление счетчика
            UpdateInfoRegister(dataGridView_count, count, 3);

            // Обновление информации о числе C
            var result = Convert.ToString(c, 2).PadLeft(32, '0'); // Преобразование числа C в двоичную строку и дополнение нулями до 32 символов

            // Очистка текстового поля для числа C
            textBox_NumberC.Text = "";

            // Копирование двоичной строки числа C
            var bufferRes = string.Copy(result);

            // Если первый бит равен 1, то число отрицательное
            if (result[0] - '0' == 1)
            {
                // Добавление знака минус в текстовое поле
                textBox_NumberC.Text = "-";

                // Замена первого бита на 0
                var sb = new StringBuilder(result);
                sb[0] = '0';
                result = sb.ToString();
            }

            // Заполнение ячеек таблицы dataGridView_number_C значениями двоичной строки числа C
            for (var i = 32 - 1; i >= 0; i--)
                dataGridView_number_C.Rows[0].Cells[i].Value = bufferRes[i];

            // Конвертация первых 16 бит числа C в десятичное число
            var numberC = Convert.ToInt32(result.Substring(0, 16), 2);

            // Формирование двоичной строки для отображения числа C в текстовом поле
            var strB = new StringBuilder().Append("00000");
            for (var i = 0; i < 5; i++)
                if ((int)(numberC / Math.Pow(10, i)) != 0)
                    strB.Remove(0, 1);

            // Добавление десятичной точки и двоичной строки в текстовое поле
            textBox_NumberC.Text += "0." + strB + numberC;
        }

        // Метод для обновления состояния памяти
        public void UpdateStateMemory(bool[] a)
        {
            // Обновление состояния радиокнопок
            radioButton_a0.Checked = a[0];
            radioButton_a1.Checked = a[1];
            radioButton_a2.Checked = a[2];
            radioButton_a3.Checked = a[3];
            radioButton_a4.Checked = a[4];

            // Обновление состояния элементов в списке
            for (var i = 0; i < a.Length; i++)
                checkedListBox_mark.SetItemChecked(i, a[i]);
        }

        // Метод для обновления состояния памяти
        public void UpdateStateMemory(ushort a)
        {
            // Обновление состояния радиокнопок в зависимости от значения переменной a
            radioButton_a0.Checked = a == 0;
            radioButton_a1.Checked = a == 1;
            radioButton_a2.Checked = a == 2;
            radioButton_a3.Checked = a == 3;
            radioButton_a4.Checked = a == 4;
        }

        // Метод для обновления информации о ключевых словах
        public void UpdateInfoKC(bool[] t, bool[] y, bool[] d, bool[] x)
        {
            // Обновление состояния элементов в списке T
            for (var i = 0; i < t.Length; i++)
                checkedListBox_T.SetItemChecked(i, t[i]);

            // Обновление состояния элементов в списке Y
            for (var i = 0; i < y.Length; i++)
                checkedListBox_Y.SetItemChecked(i, y[i]);

            // Обновление состояния элементов в списке D
            for (var i = 0; i < d.Length; i++)
                checkedListBox_D.SetItemChecked(i, d[i]);

            // Обновление состояния элементов в списке X
            for (var i = 0; i < x.Length; i++)
                checkedListBox_x.SetItemChecked(i, x[i]);
        }

        // Метод для обновления информации о предикатах X
        public void UpdateInfoXPred(bool[] x)
        {
            var q = 0; // Счетчик для индекса в списке

            // Перебор элементов массива x
            for (var i = 0; i < x.Length; i++)
            {
                // Пропускаем первые 3 элемента и элементы с индексами 8 и 9
                if (i == 0 || i == 1 || i == 2 || i == 8 || i == 9)
                {
                    continue;
                }

                // Установка флажка для элемента в списке
                checkedListBox_X_PRED.SetItemChecked(q, x[i]);

                // Увеличение счетчика
                q++;
            }
        }

        // Метод для обновления информации в регистре
        private void UpdateInfoRegister(DataGridView table, ushort value, short count)
        {
            // Преобразование числа в двоичную строку и дополнение нулями до нужной длины
            var result = Convert.ToString(value, 2).PadLeft(count, '0');

            // Заполнение ячеек таблицы значениями двоичной строки
            for (var i = count - 1; i >= 0; i--)
                table.Rows[0].Cells[i].Value = result[i];
        }

        #region Кнопки

        // Обработчик нажатия кнопки "Старт"
        private void Button_start_Click(object sender, EventArgs e)
        {
            // Если выбран режим X0-1
            if (radioButton_x0_1.Checked)
            {
                // Если выбран режим YA-OA
                if (radioButton_YA_OA.Checked)
                {
                    // Передача данных в машину управления и запуск
                    ControlMachine.InputData(A, B);
                    ControlMachine.AllRun();
                }
                else
                {
                    // Передача данных в микропрограмму и запуск
                    MicroProgram.InputData(A, B);
                    MicroProgram.AllRun();
                }
            }
        }

        // Обработчик нажатия кнопки "Такт"
        private void button_tact_Click(object sender, EventArgs e)
        {
            // Если выбран режим X0-1
            if (radioButton_x0_1.Checked)
            {
                // Если выбран режим YA-OA
                if (radioButton_YA_OA.Checked)
                {
                    // Передача данных в машину управления и запуск такта
                    ControlMachine.InputData(A, B);
                    ControlMachine.Tact();
                }
                else
                {
                    // Передача данных в микропрограмму и запуск такта
                    MicroProgram.InputData(A, B);
                    MicroProgram.Tact();
                }
            }
        }

        // Обработчик нажатия кнопки "Сброс"
        private void Reset_Click(object sender, EventArgs e)
        {
            // Сброс машины управления и микропрограммы
            ControlMachine.Reset();
            MicroProgram.Reset();

            // Обновление информации в регистрах
            UpdateInfoRegister(0, 0, 0);
        }

        #endregion

        // Обработчик нажатия на PictureBox
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}