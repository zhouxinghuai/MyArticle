using DotNetNuke.Entities.Content.Taxonomy;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Search.Entities;
using System;
using System.Collections.Generic;
using MyArticle;

namespace MyArticle
{

    public class FeatureController  : ModuleSearchBase, IPortable
    {
        public string ExportModule(int moduleId)
        {
            return "";
        }

        public void ImportModule(int moduleId, string content, string version, int userId)
        {
            return;
        }

        private static List<string> CollectHierarchicalTags(List<Term> terms)
        {
            Func<List<Term>, List<string>, List<string>> collectTagsFunc = null;
            collectTagsFunc = (ts, tags) =>
            {
                if (ts != null && ts.Count > 0)
                {
                    foreach (var t in ts)
                    {
                        tags.Add(t.Name);
                        tags.AddRange(collectTagsFunc(t.ChildTerms, new List<string>()));
                    }
                }
                return tags;
            };

            return collectTagsFunc(terms, new List<string>());
        }



        public override IList<SearchDocument> GetModifiedSearchDocuments(ModuleInfo moduleInfo, DateTime beginDateUtc)
        {
            System.Threading.Thread.Sleep(1000000);
   
            int totalCount = MyArticleManager.GetArticlesCount(moduleInfo.PortalID);
            int pageSize = 10000;
            int totalPage = totalCount / pageSize + 1;

            PortalAliasController portalAlias = new PortalAliasController();
            PortalSettings setting = DotNetNuke.Common.Globals.GetPortalSettings();
            List<PortalAliasInfo> objPortalAlias = (List<PortalAliasInfo>)portalAlias.GetPortalAliasesByPortalId(moduleInfo.PortalID);
            setting.PortalAlias = objPortalAlias[0];
            IList<SearchDocument> documents = new List<SearchDocument>();

            

            for (int pageIndex =0; pageIndex < totalPage; pageIndex++ )
            {
                List<MyArticleItem> articles = MyArticleManager.GetArticlesByPortalId(pageSize, pageIndex, moduleInfo.PortalID, 0);
                                                     
                foreach (MyArticleItem article in articles)
                {
                    SearchDocument document = new SearchDocument();
                    document.UniqueKey = article.ArticleId.ToString();
                    document.PortalId = article.PortalId;

                    document.Title = article.Title;
                    document.Body = article.Body;

                    document.Url = DotNetNuke.Common.Globals.NavigateURL(moduleInfo.TabID, setting, "", "aid=" + article.ArticleId);

                    document.ModifiedTimeUtc = article.LastModifiedOnDate;
                    document.Tags = CollectHierarchicalTags(article.ArticleContentItem.Terms);
                    documents.Add(document);
                }


            }

            return documents;

        }
    }

}
