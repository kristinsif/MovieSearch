
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieDownload
{
    using MovieSearch;
    //using DM.MovieApi.MovieDb.Movies;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

    //using MovieListCell;

    public class ImageDownloader
    {
        private IImageStorage _imageStorage;

        public ImageDownloader(IImageStorage imageStorage)
        {
            this._imageStorage = imageStorage;
        }

        public string LocalPathForFilename(string remoteFilePath)
        {
            if (remoteFilePath == null)
            {
                return string.Empty;
            }

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string localPath = Path.Combine(documentsPath, remoteFilePath.TrimStart('/'));
            return localPath;
        }

        public async Task DownloadImage(string remoteFilePath, string localFilePath, CancellationToken token)
        {
            var fileStream = new FileStream(
                                 localFilePath,
                                 FileMode.Create,
                                 FileAccess.Write,
                                 FileShare.None,
                                 short.MaxValue,
                                 true);
            try
            {
                await this._imageStorage.DownloadAsync(remoteFilePath, fileStream, token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        
        public async Task<string> saveImageAsync(string remotePath)
        {
            var localPath = LocalPathForFilename(remotePath);
            await DownloadImage(remotePath, localPath, new CancellationTokenSource().Token);

            return localPath;
        }
       
        public async Task<List<Movie>> getLocalPath(List<Movie> movieList)
        {

            foreach(Movie movie in movieList)
            {
                var localPath = await saveImageAsync(movie.ImageUrl);
                movie.localImagePath = localPath;
            }
            
            return movieList;
        }

        public async Task<List<MovieDetail>> getImageUrl(List<MovieDetail> movieDetail)
        {        
            foreach (MovieDetail movie in movieDetail)
            {
                var localPath = await saveImageAsync(movie.ImageUrl);
                movie.ImageUrl = localPath;
            }

            return movieDetail;
        }


    }
}
