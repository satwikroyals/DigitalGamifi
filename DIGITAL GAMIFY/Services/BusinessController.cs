using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Http.Cors;
using DIGITAL_GAMIFY.BAL;
using DIGITAL_GAMIFY.Entities;
using System.Text.RegularExpressions;
using System.IO;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing;
using System.Drawing.Imaging;
using DIGITAL_GAMIFY.Code;

namespace DIGITAL_GAMIFY.Services
{
     //[EnableCors(origins: "http://www.gamesnatcherz.com,http://gamesnatcherz.com", headers: "*", methods: "*")]
    public class BusinessController : ApiController
    {
        private BusinessManager objbm = new BusinessManager();
        [Route("api/GetBusinessById")]
        [HttpGet]
        public object GetBusinessById(Int64 Id)
        {
            try
            {
                BusinessEntity be = objbm.GetBusinessById(Id);
                object res = new
                {
                    Status = true,
                    Success = be == null ? false : true,
                    Data = be
                };
                return res;
            }
            catch (Exception ex)
            {
                object res = new
                {
                    Status = false,
                    Data = ex
                };
                return res;
            }
        }        

        [Route("api/AdminGetBusiness")]
        [HttpPost]
        public List<BusinessEntity> AdminGetBusiness(BusinessListParamsEntity p)
        {
            return objbm.AdminGetBusiness(p);
        }

        [Route("api/GetSwipeandWinClaimedBusiness")]
        [HttpGet]
        public List<BusinessEntity> GetSwipeandWinClaimedBusiness(Int32 cid)
        {
            return objbm.GetSwipeandWinClaimedBusiness(cid);
        }
        [Route("api/GetBusinessAppLogin")]
        [HttpGet]
        public object GetBusinessAppLogin(string un, string pwd, int utype)
        {
            BusinessEntity ce = new BusinessEntity();
            StatusResponse se = new StatusResponse();
            try
            {
                ce = objbm.GetBusinessAppLogin(un, pwd, utype);
                if (ce == null)
                {
                    se.StatusCode = -1;
                    se.StatusMessage = "It seems your username and/or password do not match—please try again.";
                }
                object res = new object();
                res = new
                {
                    Result = se,
                    Details = ce,
                };
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [Route("api/GetGameResultsByBusiness")]
        [HttpGet]
        public object GetGameResultsByBusiness(Int64 bid)
        {
            try
            {
                RedeemDetailsEntity ge = objbm.GetGameResultsByBusiness(bid);
                object res = new
                {
                    Status = true,
                    Success = ge == null ? false : true,
                    Data = ge
                };
                return res;
            }
            catch (Exception ex)
            {
                object res = new
                {
                    Status = false,
                    Data = ex
                };
                return res;
            }
        }
        [Route("api/RedeemGamePrizeAction")]
        [HttpGet]
        public StatusResponse RedeemGamePrizeAction(Int32 gid, Int32 cid, int action)
        {
            try
            {
                return objbm.RedeemGamePrize(gid, cid, action);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("api/CheckRedeemCode")]
        [HttpGet]
        public object CheckRedeemCode(string Rdcode)
        {
            StatusResponse se = new StatusResponse();
            try
            {
                BusinessGameResultEntity bge = objbm.CheckRedeemCode(Rdcode);
                if (bge == null)
                {
                    se.StatusCode = -1;
                    se.StatusMessage = "Prize Redeemed.";
                }
                else if (bge.StatusMessage != null)
                {
                    se.StatusCode = -1;
                    se.StatusMessage = bge.StatusMessage;
                }
                object res = new object();
                res = new
                {
                    Status = se,
                    Details = bge,
                };
                return res;
            }
            catch (Exception ex)
            {
                object res = new
                {
                    Status = false,
                    Data = ex
                };
                return res;
            }
        }
        [Route("api/GetBusinessProfile")]
        [HttpGet]
        public object GetBusinessProfile(Int64 Id)
        {
            try
            {
                BusinessEntity be = objbm.GetBusinessById(Id);
                object res = new
                {
                    Status = true,
                    Success = be == null ? false : true,
                    Data = be
                };
                return res;
            }
            catch (Exception ex)
            {
                object res = new
                {
                    Status = false,
                    Data = ex
                };
                return res;
            }
        }

        [Route("api/BusinessRegister")]
        [HttpPost]
        public BusinessEntity BusinessRegister(BusinessEntity busi)
        {
            BusinessEntity st = new BusinessEntity();
            BusinessEntity bEntity = new BusinessEntity();
            ///byte[] image64 = Convert.FromBase64String(convert);
            if (busi.imgfile != "")
            {
                string fname = (busi.BusinessName.ToString()).Replace(" ","");
                string convert = Regex.Replace(busi.imgfile, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
                //string convert = cusimage.Image.Replace("data:image/{format};base64,/9j/", String.Empty);
                byte[] imagedata = Convert.FromBase64String(convert);
                string generatefilename = fname.ToString();
                string fileextence = ".jpg";
                bEntity.AdminId = busi.AdminId;
                bEntity.BusinessId = busi.BusinessId;
                bEntity.FirstName = busi.FirstName;
                bEntity.LastName = busi.LastName;
                bEntity.BusinessId = busi.BusinessId;
                bEntity.Email = busi.Email;
                bEntity.Mobile = busi.Mobile;
                bEntity.BusinessName = busi.BusinessName;
                bEntity.BusinessTypeId = busi.BusinessTypeId;
                bEntity.ZipCode = busi.ZipCode;
                bEntity.IsActive = busi.IsActive;
                bEntity.UserName = busi.UserName;
                bEntity.Password = busi.Password;
                bEntity.Address = busi.Address;
                bEntity.Latitude = busi.Latitude;
                bEntity.Longitude = busi.Longitude;
                bEntity.Logo = generatefilename + fileextence;
                st = objbm.AddBusiness(bEntity);
                if (imagedata != null && imagedata.Length > 0 && st.BusinessId > 0)
                {
                    string strfilepath = System.Web.Hosting.HostingEnvironment.MapPath("~/ApplicationFiles/business/" + st.BusinessId.ToString() + "/") + generatefilename + fileextence;
                    FileStream targetStream = null;
                    MemoryStream ms = new MemoryStream(imagedata);
                    Stream Sourcestream = ms;
                    string uploadfolder = System.Web.Hosting.HostingEnvironment.MapPath("~/ApplicationFiles/business/" + st.BusinessId.ToString() + "/");
                    if (!Directory.Exists(uploadfolder))
                    {
                        Directory.CreateDirectory(uploadfolder);
                    }
                    string filename = generatefilename + fileextence;

                    string filePath = Path.Combine(uploadfolder, filename);
                    // write file using stream.
                    using (targetStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        const int bufferLen = 4096;
                        byte[] buffer = new byte[bufferLen];
                        int count = 0;
                        int totalBytes = 0;
                        while ((count = Sourcestream.Read(buffer, 0, bufferLen)) > 0)
                        {

                            totalBytes += count;
                            targetStream.Write(buffer, 0, count);

                        }

                        targetStream.Close();

                        Sourcestream.Close();

                    }
                }
                string dir = System.Web.Hosting.HostingEnvironment.MapPath("~/ApplicationFiles/business/" + st.BusinessId.ToString() + "/");
                string QRCodeUrl = Globalsettings.GetBusinessQrCodeUrl(st.BusinessId);

                QRCodeEncoder encoder = new QRCodeEncoder();
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                encoder.QRCodeScale = 10;
                Bitmap img = encoder.Encode(QRCodeUrl);
                Graphics g = Graphics.FromImage(img);
                img.Save(dir + "QR.jpg", ImageFormat.Jpeg);
            }
            else
            {
                st = objbm.AddBusiness(busi);
                string dir = System.Web.Hosting.HostingEnvironment.MapPath("~/ApplicationFiles/business/" + st.BusinessId.ToString() + "/");
                string QRCodeUrl = Globalsettings.GetBusinessQrCodeUrl(st.BusinessId);

                QRCodeEncoder encoder = new QRCodeEncoder();
                encoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                encoder.QRCodeScale = 10;
                Bitmap img = encoder.Encode(QRCodeUrl);
                Graphics g = Graphics.FromImage(img);
                img.Save(dir + "QR.jpg", ImageFormat.Jpeg);
            }
            return st;
        }

    }
}