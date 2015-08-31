using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wmcb.model;

namespace wmcb.repo
{
    public class NewsFeedRepo
    {
        public List<NewsView> getLastestNewsFeeds(int NewsFeedCount)
        {
           List<NewsView> news = new List<NewsView>();
           using (var context = new wmcbContext())
           {
               var feeds = context.NewsFeeds
                   .Join(context.Users, nf => nf.CreatedBy, u => u.ID, (nf, u) => new { Feed = nf, User = u })
                            .OrderByDescending(n => n.Feed.CreatedOn)
                            .Select(n => new NewsView()
                            {
                                Headline = n.Feed.Headline,
                                Content = n.Feed.Content,
                                CreatedBy = n.User.FirstName,// + ' ' + n.User.LastName,
                                CreatedOn = n.Feed.CreatedOn
                            })
                            .Take(NewsFeedCount);
               if (feeds != null)
                   news = feeds.ToList();
           }
           return news;
       }
        public List<NewsView> getAllNewsFeeds()
        {
            List<NewsView> news = new List<NewsView>();
            using (var context = new wmcbContext())
            {
                var feeds = context.NewsFeeds
                    .Join(context.Users, nf => nf.CreatedBy, u => u.ID, (nf, u) => new { Feed = nf, User = u })
                             .OrderByDescending(n => n.Feed.CreatedOn)
                             .Select(n => new NewsView()
                             {
                                 Headline = n.Feed.Headline,
                                 Content = n.Feed.Content,
                                 CreatedBy = n.User.FirstName,// + ' ' + n.User.LastName,
                                 CreatedOn = n.Feed.CreatedOn
                                
                             });
                if (feeds != null)
                    news = feeds.ToList();
            }
            return news;
        }
        public Result AddNewsFeed(NewsFeed news)
        {
            var result = new Result();
            using (var context = new wmcbContext())
            {
                try
                {
                    context.NewsFeeds.Add(news);
                    context.SaveChanges();
                    result.Code = 0;
                    result.Message = "success";
                }
                catch(Exception ex){
                    result.Code = -1;
                    result.Message = ex.Message;
                }
            }
            return result;
        }
    }
}
