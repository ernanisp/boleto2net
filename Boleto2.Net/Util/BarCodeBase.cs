using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Boleto2Net
{
    public abstract class BarCodeBase
    {
        #region Variables
        private string _code;
        private int _height;
        private int _digits;

        private int _thin;
        private int _full;

        protected int XPos = 0;
        protected int YPos = 0;

        private string _contenttype;

        protected Brush Black = Brushes.Black;
        protected Brush White = Brushes.White;

        #endregion

        #region Property
        /// <summary>
        /// The Barcode.
        /// </summary>
        public string Code
        {
            get
            {
                try
                {
                    return _code;
                }
                catch (Exception ex)
                {
                    return "";
                }
            }
            set
            {
                try
                {
                    _code = value;
                }
                catch (Exception ex)
                {
                    _code = null;
                }
            }
        }
        /// <summary>
        /// The width of the thin bar (pixels).
        /// </summary>
        public int Width
        {
            get
            {
                try
                {
                    return _thin;
                }
                catch (Exception ex)
                {
                    return 1;
                }
            }
            set
            {
                try
                {
                    int temp = value;
                    _thin = temp;
                    //					_half = temp * 2;
                    _full = temp * 3;
                }
                catch (Exception ex)
                {
                    int temp = 1;
                    _thin = temp;
                    //					_half = temp * 2;
                    _full = temp * 3;
                }
            }
        }
        /// <summary>
        /// The Height of barcode (pixels).
        /// </summary>
        public int Height
        {
            get
            {
                try
                {
                    return _height;
                }
                catch (Exception ex)
                {
                    return 15;
                }
            }
            set
            {
                try
                {
                    _height = value;
                }
                catch (Exception ex)
                {
                    _height = 1;
                }
            }
        }
        /// <summary>
        /// Number of digits of the barcode.
        /// </summary>
        public int Digits
        {
            get
            {
                try
                {
                    return _digits;
                }
                catch (Exception ex)
                {
                    return 0;
                }
            }
            set
            {
                try
                {
                    _digits = value;
                }
                catch (Exception ex)
                {
                    _digits = 0;
                }
            }
        }
        /// <summary>
        /// Content type of code. Default: image/jpeg
        /// </summary>
        public string ContentType
        {
            get
            {
                try
                {
                    if (_contenttype == null)
                        return "image/jpeg";
                    return _contenttype;
                }
                catch (Exception ex)
                {
                    return "image/jpeg";
                }
            }
            set
            {
                try
                {
                    _contenttype = value;
                }
                catch (Exception ex)
                {
                    _contenttype = "image/jpeg";
                }
            }
        }
        protected int Thin
        {
            get
            {
                try
                {
                    return _thin;
                }
                catch (Exception ex)
                {
                    return 1;
                }
            }
        }
        protected int Full
        {
            get
            {
                try
                {
                    return _full;
                }
                catch (Exception ex)
                {
                    return 3;
                }
            }
        }
        #endregion
        protected virtual byte[] ToByte(Bitmap bitmap)
        {
            MemoryStream mstream = new MemoryStream();
            ImageCodecInfo myImageCodecInfo = GetEncoderInfo(ContentType);

            EncoderParameter myEncoderParameter0 = new EncoderParameter(Encoder.Quality, (long)100);
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            myEncoderParameters.Param[0] = myEncoderParameter0;

            bitmap.Save(mstream, myImageCodecInfo, myEncoderParameters);

            return mstream.GetBuffer();
        }
        private ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        protected virtual void DrawPattern(ref Graphics g, string pattern)
        {
            int tempWidth;

            for (int i = 0; i < pattern.Length; i++)
            {
                if (pattern[i] == '0')
                    tempWidth = _thin;
                else
                    tempWidth = _full;

                if (i % 2 == 0)
                    g.FillRectangle(Black, XPos, YPos, tempWidth, _height);
                else
                    g.FillRectangle(White, XPos, YPos, tempWidth, _height);

                XPos += tempWidth;
            }
        }
    }
}
