namespace ContosoCrafts.WebSite.Middleware
{
	public class Handle405Middleware
	{
		private readonly RequestDelegate next;

		public Handle405Middleware(RequestDelegate next)
		{
			this.next = next;
		}
		public async Task Invoke(HttpContext context)
		{
			await next(context);
			//Replace 405 by 404
			if (context.Response.StatusCode == StatusCodes.Status405MethodNotAllowed)
				context.Response.StatusCode = StatusCodes.Status404NotFound;
		}
	}
}
