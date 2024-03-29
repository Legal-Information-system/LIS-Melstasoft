﻿using LegalSystemCore.Common;
using LegalSystemCore.Domain;
using LegalSystemCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace LegalSystemCore.Controller
{

    public interface ICourtLocationController
    {
        int Save(CourtLocation courtlocation);
        int Delete(CourtLocation courtlocation);

        int Update(CourtLocation courtlocation, int courtId, int locationId);
        List<CourtLocation> GetCourtLocationList(bool with0);
        List<CourtLocation> GetCourtLocationListFilter(int withCourtId);
    }

    public class CourtLocationControllerImpl : ICourtLocationController
    {
        ICourtLocationDAO courtlocationDAO = DAOFactory.CreatCourtLocationDAO();
        public List<CourtLocation> GetCourtLocationList(bool with0)
        {
            Common.DbConnection dbConnection = null;
            List<CourtLocation> listcourtlocation = new List<CourtLocation>();
            try
            {
                dbConnection = new Common.DbConnection();
                listcourtlocation = courtlocationDAO.GetCourtLocationList(with0, dbConnection);

                ICourtDAO courtDAO = DAOFactory.CreateCourtDAO();
                List<Court> listCourt = courtDAO.GetCourtList(true, dbConnection);

                foreach (var courtlocation in listcourtlocation)
                {
                    courtlocation.court = listCourt.Where(x => x.CourtId == courtlocation.CourtId).Single();
                }

                ILocationDAO locationDAO = DAOFactory.CreateLocationDAO();
                List<Location> listLocation = locationDAO.GetLocationList(true, dbConnection);

                foreach (var courtlocation in listcourtlocation)
                {
                    courtlocation.location = listLocation.Where(x => x.LocationId == courtlocation.LocationId).Single();
                }


            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
            return listcourtlocation;
        }

        public List<CourtLocation> GetCourtLocationListFilter(int withCourtId)
        {
            Common.DbConnection dbConnection = null;
            List<CourtLocation> listCourtLocation = new List<CourtLocation>();
            try
            {
                dbConnection = new Common.DbConnection();
                listCourtLocation = courtlocationDAO.GetCourtLocationListFilter(withCourtId, dbConnection);


                ILocationDAO locationDAO = DAOFactory.CreateLocationDAO();
                List<Location> listLocatioin = locationDAO.GetLocationList(true, dbConnection);

                foreach (var courtLocation in listCourtLocation)
                {
                    courtLocation.location = listLocatioin.Where(x => x.LocationId == courtLocation.LocationId).Single();
                }

            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
            return listCourtLocation;
        }

        public int Save(CourtLocation courtlocation)
        {
            Common.DbConnection dbConnection = null;
            List<CourtLocation> courtLocationList = new List<CourtLocation>();
            int flage = 0;
            try
            {
                dbConnection = new Common.DbConnection();
                courtLocationList = courtlocationDAO.GetCourtLocationList(true, dbConnection);

                foreach (var cl in courtLocationList)
                {
                    if (cl.CourtId == courtlocation.CourtId && cl.LocationId == courtlocation.LocationId)
                        flage = 1;
                }

                if (flage == 0)
                    return courtlocationDAO.Save(courtlocation, dbConnection);
                else
                    return courtlocationDAO.Enable(courtlocation, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbConnection.Commit();
                }
            }
        }

        public int Delete(CourtLocation courtlocation)
        {
            Common.DbConnection dbconnection = null;
            try
            {
                dbconnection = new Common.DbConnection();
                return courtlocationDAO.Delete(courtlocation, dbconnection);
            }
            catch (Exception)
            {
                dbconnection.RollBack();
                throw;
            }
            finally
            {
                if (dbconnection.con.State == System.Data.ConnectionState.Open)
                {
                    dbconnection.Commit();
                }
            }
        }

        public int Update(CourtLocation courtlocation, int courtId, int locationId)
        {
            Common.DbConnection dbConnection = null;
            try
            {
                dbConnection = new Common.DbConnection();
                return courtlocationDAO.Update(courtlocation, courtId, locationId, dbConnection);
            }
            catch (Exception)
            {
                dbConnection.RollBack();
                throw;
            }
            finally
            {
                if (dbConnection.con.State == System.Data.ConnectionState.Open)
                    dbConnection.Commit();
            }
        }
    }
}
