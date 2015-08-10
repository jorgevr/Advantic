using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PostgreSqlClient.Entities;

namespace PostgreSqlClient.Repositories
{
    public interface IRoomRepository
    {
        bool Exists(Room room);
        void Save(Room room);
        void SaveList(IList<Room> roomList);
        void Update(Room room);
        Room Get(String roomId);
        IList<Room> GetAll();
        
    }

    public class RoomRepository : IRoomRepository
    {
        private RepositoryHelper _repositoryHelper;


        public RoomRepository(RepositoryHelper repositoryHelper) 
        {
            _repositoryHelper= repositoryHelper;
        }

        #region IRoomRepository Methods

        public Room Get(string roomId) 
        {
            return _repositoryHelper.GetRoom(roomId);
        }

        public IList<Room> GetAll()
        {
            return _repositoryHelper.GetAllRoom();
        }

        public bool Exists(Room room) 
        {
            return Get(room.Id) != null;
        }
        public void Save(Room room)
        {
            _repositoryHelper.SaveRoom(room);
        }
        public void SaveList(IList<Room> roomList) 
        {
            _repositoryHelper.SaveRoomList(roomList);
        }
        public void Update(Room room) 
        {
            _repositoryHelper.UpdateRoom(room);
        }
       
        #endregion

    }
}
