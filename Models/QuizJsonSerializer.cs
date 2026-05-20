namespace QuizzesApp.Models
{
    internal class QuizJsonSerializer
    {
        public object obj = new();
        public bool isJson = false;

        public QuizJsonSerializer()
        {
        }

        public void DeserializeToJson(ref readonly string str, out object? obj)
        {
            obj = null;
            if (string.IsNullOrWhiteSpace(str)) { return; }

            try
            {
                obj = System.Text.Json.JsonSerializer.Deserialize<object>(str)!;
                if (this.obj != null)
                {
                    isJson = true;
                    obj = this.obj;
                }
                else obj = null;
            }
            catch (ArgumentNullException ex)
            {
                WriteExceptionToConsole(ex);
            }
            catch (System.Text.Json.JsonException ex)
            {
                WriteExceptionToConsole(ex);
            }
            catch (Exception ex)
            {
                WriteExceptionToConsole(ex);
            }
        }

        public void DeserializeToJson(ref readonly FileStream stream, out object? obj)
        {
            obj = null;
            if (Equals(stream, null)) { return; }

            try
            {
                this.obj = System.Text.Json.JsonSerializer.Deserialize<object>(stream)!;
                if (this.obj != null)
                {
                    isJson = true;
                    obj = this.obj;
                }
                else obj = null;
            }
            catch (ArgumentNullException ex)
            {
                WriteExceptionToConsole(ex);
            }
            catch (System.Text.Json.JsonException ex)
            {
                WriteExceptionToConsole(ex);
            }
            catch (Exception ex)
            {
                WriteExceptionToConsole(ex);
            }
        }

        public void DeserializeToJson(ref readonly FileStream stream, out QuizData? obj)
        {
            obj = null;
            if (Equals(stream, null)) { return; }

            try
            {
                this.obj = System.Text.Json.JsonSerializer.Deserialize<QuizData>(stream)!;
                if (this.obj != null)
                {
                    isJson = true;
                    obj = (QuizData)this.obj;
                }
                else obj = null;
            }
            catch (ArgumentNullException ex)
            {
                WriteExceptionToConsole(ex);
            }
            catch (System.Text.Json.JsonException ex)
            {
                WriteExceptionToConsole(ex);
            }
            catch (Exception ex)
            {
                WriteExceptionToConsole(ex);
            }
        }

        private void WriteExceptionToConsole(Exception e)
        {
            if (e.InnerException == null)
                Console.WriteLine("Exception: {0}\n{1}", e.ToString(), e.StackTrace);
            else
                Console.WriteLine("Exception: {0}\n{1}\n{2}", e.ToString(), e.StackTrace, e.InnerException);
        }

    }
}
