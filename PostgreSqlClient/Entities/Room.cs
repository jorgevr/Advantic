using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PostgreSqlClient.Entities
{
    public class Room
    {
        #region Public Virtual Properties

        public virtual String  Id { get; set; }
        public virtual Cell Cell { get; set; }
        public virtual Double Surface { get; set; }
        public virtual Double Volumen { get; set; }
        public virtual int MaxOcup { get; set; }
        public virtual String Description { get; set; }
        public virtual DateTime LocalInsertTime { get; set; }
        public virtual String InsertUser { get; set; }
        public virtual DateTime UpdateLocalDateTime { get; set; }
        public virtual String UpdateUser { get; set; }

        #endregion
        public Room(string roomId) 
        {
            Id = roomId;
        }

        public Room()
        {
        }
        #region Public Override Methods

        public override bool Equals(object obj)
        {
            Room r = obj as Room;
            return r != null
                && r.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

    }
}
