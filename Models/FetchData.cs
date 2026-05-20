namespace QuizzesApp.Models
{
    internal sealed class FetchData
    {
        public QuizData? GetFromFileToJson(string path)
        {
            if (path == null) { throw new ArgumentNullException("path"); }

            FileStream quizFileData;
            QuizData? Data;

            try
            {
                //This function calls to get stream data from a json file
                quizFileData = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.None);

                //Call serializer to deserialize the file
                QuizJsonSerializer quizJsonSerializer = new QuizJsonSerializer();
                quizJsonSerializer.DeserializeToJson(ref quizFileData, out Data);

                quizFileData.Close();

                //Confirm the serializer output
                if (!quizJsonSerializer.isJson) return null;

                return Data;
            }
            catch (ArgumentNullException ex)
            {
                WriteExceptionToConsole(ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                WriteExceptionToConsole(ex);
            }
            catch (ArgumentException ex)
            {
                WriteExceptionToConsole(ex);
            }
            catch (PathTooLongException ex)
            {
                WriteExceptionToConsole(ex);
                throw;
            }
            catch (DirectoryNotFoundException ex)
            {
                WriteExceptionToConsole(ex);
                throw;
            }
            catch (FileNotFoundException ex)
            {
                WriteExceptionToConsole(ex);
                throw;
            }
            catch (IOException ex)
            {
                WriteExceptionToConsole(ex);
            }
            catch (UnauthorizedAccessException ex)
            {
                WriteExceptionToConsole(ex);
                throw;
            }
            catch (NotSupportedException ex)
            {
                WriteExceptionToConsole(ex);
            }
            catch (Exception ex)
            {
                WriteExceptionToConsole(ex);
            }

            return null;
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
