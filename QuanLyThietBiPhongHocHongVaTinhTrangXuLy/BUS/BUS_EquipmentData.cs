using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTL;
using DAL;
namespace BUS
{
    public class BUS_EquipmentData
    {
        EQUIPMENT ac = new EQUIPMENT();
        private static BUS_EquipmentData _Instance;
        public static BUS_EquipmentData Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new BUS_EquipmentData();
                return _Instance;
            }
            private set { _Instance = value; }
        }
        public List<EquipmentShow> BUS_EquipmentShow(string text)
        {
            List<EquipmentShow> list = new List<EquipmentShow>();
            foreach (EquipmentShow item in DAL_EquipmentData.Instance.DAL_EquipmentShow())
            {
                if (item.roomID.Contains(text))
                {
                    list.Add(item);
                }
            }
            return list;
        }
        public List<EquipmentShow> BUS_GetEquipmentShow()
        {
            return DAL_EquipmentData.Instance.DAL_GetEquipmentShow();
        }
        public List<EquipmentShow> BUS_Sort(string cbbitem)
        {
            string item = cbbitem;
            switch (item)
            {
                case "equipmetId":
                    {
                        return BUS_GetEquipmentShow().OrderBy(o => o.equipmentID).ToList();
                    }
                case "equipmentName":
                    {
                        return BUS_GetEquipmentShow().OrderBy(o => o.equipmentName).ToList();
                    }
                case "roomId":
                    {
                        return BUS_GetEquipmentShow().OrderBy(o => o.roomID).ToList();
                    }
                case "dateOfInstallation":
                    {
                        return BUS_GetEquipmentShow().OrderBy(o => o.dateOfInstallation).ToList();
                    }
                case "company":
                    {
                        return BUS_GetEquipmentShow().OrderBy(o => o.company).ToList();
                    }
                default:
                    {
                        return BUS_GetEquipmentShow();
                    }
            }
        }
        public void BUS_SETEQUIPMENT(EQUIPMENT eq)
        {
            DAL_EquipmentData.Instance.DAL_SetEquipment(eq);
        }
        public void BUS_DELETEEQUIPMENT(string equipmentid)
        {
            DAL_EquipmentData.Instance.DAL_DeleteEquipment(equipmentid);
        }
        public void BUS_UPDATEEQUIPMENT(EQUIPMENT eq2)
        {
            DAL_EquipmentData.Instance.DAL_UpdateEquipment(eq2);
        }
        public EquipmentShow BUS_getEquipmentByIDEquipment(string equipmentid)
        {
            foreach (EquipmentShow item in DAL_EquipmentData.Instance.DAL_EquipmentShow())
            {
                if (item.equipmentID == equipmentid)
                {
                    return item;
                }
            }
            return null;
        }
        public int BUS_CHECKEQUIPMENT(EQUIPMENT eq)
        {
            return DAL_EquipmentData.Instance.DAL_CheckEquipment(eq);
        }
        public List<EquipmentShow> BUS_ShowEquipmentByRoomId(string roomId)
        {
            return DAL_EquipmentData.Instance.DAL_ShowEquipmentByRoomId(roomId);
        }
        public List<EquipmentShow> BUS_EquipmentShow()
        {
            return DAL_EquipmentData.Instance.DAL_EquipmentShow();
        }
    }
}
