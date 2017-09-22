namespace WebApplication
{
    public class DefaultController
    {
		public string Index()
		{
			return $"Hello from {this.GetType().ToString()}!";
		}
    }
}