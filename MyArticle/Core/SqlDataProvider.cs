
using System;
using System.Data;
using System.Data.SqlClient;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Framework.Providers;
using Microsoft.ApplicationBlocks.Data;
using DotNetNuke.Entities.Portals;

namespace MyArticle
{

    /// -----------------------------------------------------------------------------
    /// <summary>
    /// SQL Server implementation of the abstract DataProvider class
    /// </summary>
    /// -----------------------------------------------------------------------------
    public class SqlDataProvider
    {


        private static SqlDataProvider _provider;

        public static SqlDataProvider Instance()
        {
            if (_provider == null)
            {
                const string assembly = "MyArticle.SqlDataprovider,MyArticle";
                Type objectType = Type.GetType(assembly, true, true);

                if (objectType != null)
                {
                    _provider = (SqlDataProvider)Activator.CreateInstance(objectType);

             DataCache.SetCache(objectType.FullName, _provider);

                  
                }
            }

            return _provider;
        }

        public string ConnectionString
        {
            get
            {


               

            return Config.GetConnectionString();


            }
        }

        public IDataReader GetArticlesByPortalId(int pageSize, int pageIndex, int portalId, ResultSortType sort)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "MyArticle_spGetArticlesByPortalId",
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@PageIndex", pageIndex),
                new SqlParameter("@Sort", sort),
                new SqlParameter("@PortalId", portalId)
                );
        }

        public IDataReader GetArticlesByTitle(int pageSize, int pageIndex, int portalId, ResultSortType sort, string title)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "MyArticle_spGetArticlesByTitle",
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@PageIndex", pageIndex),
                new SqlParameter("@PortalId", portalId),
                 new SqlParameter("@Sort", sort),
                new SqlParameter("@Title", title));
        }

        public IDataReader GetArticlesByTag(int pageSize, int pageIndex, int portalId, ResultSortType sort, string tag)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "MyArticle_spGetArticlesByTag",
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@PageIndex", pageIndex),
                new SqlParameter("@PortalId", portalId),
                 new SqlParameter("@Sort", sort),
                new SqlParameter("@Tag", tag));
        }

        internal IDataReader GetArticlesByCreatedOnDate(int pageSize, int pageIndex, int portalId, ResultSortType sort, DateTime startDate, DateTime endDate)
        {
            SqlParameter startParam = new SqlParameter("@StartDate", SqlDbType.DateTime);
            startParam.Value = startDate;

            SqlParameter endParam = new SqlParameter("@EndDate", SqlDbType.DateTime);
            startParam.Value = endDate;

            return SqlHelper.ExecuteReader(ConnectionString, "MyArticle_spGetArticlesByCreatedOnDate",
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@PageIndex", pageIndex),
                new SqlParameter("@PortalId", portalId),
                new SqlParameter("@Sort", sort),
                startParam,
                endParam);
        }

        internal IDataReader GetArticlesByLastModifiedOnDate(int pageSize, int pageIndex, int portalId, ResultSortType sort, DateTime startDate, DateTime endDate)
        {


            return SqlHelper.ExecuteReader(ConnectionString, "MyArticle_spGetArticlesByLastModifiedOnDate",
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@PageIndex", pageIndex),
                new SqlParameter("@PortalId", portalId),
                new SqlParameter("@Sort", sort),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate));
        }

        public IDataReader GetArticlesByAuthor(int pageSize, int pageIndex, int portalId, ResultSortType sort, string author)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "MyArticle_spGetArticlesByAuthor",
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@PageIndex", pageIndex),
                new SqlParameter("@PortalId", portalId),
                 new SqlParameter("@Sort", sort),
                new SqlParameter("@Author", author));
        }


        public IDataReader GetArticlesByUserId(int pageSize, int pageIndex, int portalId, ResultSortType sort, int userId)
        {
            return SqlHelper.ExecuteReader(ConnectionString, "MyArticle_spGetArticlesByUserId",
                new SqlParameter("@PageSize", pageSize),
                new SqlParameter("@PageIndex", pageIndex),
                new SqlParameter("@PortalId", portalId),
                 new SqlParameter("@Sort", sort),
                new SqlParameter("@UserId", userId));
        }

        public IDataReader GetArticleByArticleId(int articleId)
        {

            return SqlHelper.ExecuteReader(ConnectionString, "MyArticle_spGetArticleByArticleId",
                new SqlParameter("@ArticleId", articleId));
        }

        public int AddArticle(MyArticleItem a)
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, CommandType.StoredProcedure, "MyArticle_spAddArticle"
                , new SqlParameter("@PortalId", a.PortalId)
                , new SqlParameter("@Title", a.Title)
                , new SqlParameter("@IsPublished", a.IsPublished)
                , new SqlParameter("@Body", a.Body)
                , new SqlParameter("@CreatedByUserId", a.CreatedByUserId)
                , new SqlParameter("@Description", a.Description)
                , new SqlParameter("@CreatedOnDate", a.CreatedOnDate)
                , new SqlParameter("@LastModifiedOnDate", a.LastModifiedOnDate)
                , new SqlParameter("@ThumbnailUrl", a.ThumbnailUrl)
                , new SqlParameter("@LastModifiedByUserId", a.LastModifiedByUserId)
                , new SqlParameter("@Author", a.Author)
                , new SqlParameter("@IsComment", a.IsComment)
                ));
        }

        public void DeleteArticle(int articleId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "MyArticle_spDeleteArticle", new SqlParameter("@ArticleId", articleId));
        }

        public void UpdateArticle(MyArticleItem a)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "MyArticle_spUpdateArticle"
                , new SqlParameter("@ArticleId", a.ArticleId)
                , new SqlParameter("@PortalId", a.PortalId)
               , new SqlParameter("@Title", a.Title)
               , new SqlParameter("@IsPublished", a.IsPublished)
               , new SqlParameter("@Body", a.Body)
               , new SqlParameter("@LastModifiedOnDate", a.LastModifiedOnDate)
               , new SqlParameter("@LastModifiedByUserId", a.LastModifiedByUserId)
               , new SqlParameter("@ContentItemId", a.ContentItemId)
                , new SqlParameter("@Description", a.Description)
                , new SqlParameter("@ThumbnailUrl", a.ThumbnailUrl)
                , new SqlParameter("@Author", a.Author)
                , new SqlParameter("@IsComment", a.IsComment)
               );
        }

        public void AddClickCount(int articleId)
        {
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.StoredProcedure, "MyArticle_spUpdateClickCount"
              , new SqlParameter("@ArticleId", articleId)

             );
        }

        public int GetArticlesCount(int portalId)
        {

            return (int)SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, "SELECT COUNT(*) FROM MyArticle WHERE   PortalId = " + portalId.ToString());


        }

    }

}



