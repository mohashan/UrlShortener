public class RegisterUrl{
    public string Url { get; set; }
    public bool Validate(){
        if (Url == null)
        {
            return false;
        }
        if (Url.Length > 2000)
        {
            return false;
        }
        return true;
    }
}