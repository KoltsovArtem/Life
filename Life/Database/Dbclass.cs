using System.Data.SQLite;
using System.Windows.Controls;

public class SQLiteManager
{
    private static string connectionString;

    public SQLiteManager(string dbFilePath)
    {
        connectionString = $"Data Source={dbFilePath};Version=3;";
    }

    public void CreateDatabase()
    {
        SQLiteConnection.CreateFile(connectionString);
    }

    public static void SaveArray(int[,] array)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            // Создаем таблицу для хранения массива
            string createTableQuery = "CREATE TABLE IF NOT EXISTS Array (Row INT, Column INT, Value INT)";
            SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, connection);
            createTableCommand.ExecuteNonQuery();

            // Очищаем таблицу перед сохранением новых данных
            string clearTableQuery = "DELETE FROM Array";
            SQLiteCommand clearTableCommand = new SQLiteCommand(clearTableQuery, connection);
            clearTableCommand.ExecuteNonQuery();

            // Сохраняем значения массива в таблицу
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    string insertQuery = "INSERT INTO Array (Row, Column, Value) VALUES (@row, @column, @value)";
                    SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection);
                    insertCommand.Parameters.AddWithValue("@row", i);
                    insertCommand.Parameters.AddWithValue("@column", j);
                    insertCommand.Parameters.AddWithValue("@value", array[i, j]);
                    insertCommand.ExecuteNonQuery();
                }
            }
        }
    }

    public static void ReadArray(int[,] array, Grid[,] grid)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string selectQuery = "SELECT Row, Column, Value FROM Array";
            SQLiteCommand selectCommand = new SQLiteCommand(selectQuery, connection);
            SQLiteDataReader reader = selectCommand.ExecuteReader();

            // Определяем размеры массива
            int maxRow = -1;
            int maxColumn = -1;

            while (reader.Read())
            {
                int row = reader.GetInt32(0);
                int column = reader.GetInt32(1);

                if (row > maxRow)
                    maxRow = row;

                if (column > maxColumn)
                    maxColumn = column;
            }

            // Создаем массив с прочитанными значениями
            //int[,] array = new int[maxRow + 1, maxColumn + 1];

            reader.Close();

            // Заполняем массив значениями из базы данных
            reader = selectCommand.ExecuteReader();

            while (reader.Read())
            {
                int row = reader.GetInt32(0);
                int column = reader.GetInt32(1);
                int value = reader.GetInt32(2);

                array[row, column] = value;
                grid[row, column].Tag = array[row, column] != 0 ? "alive" : "dead";
            }
        }
    }
}