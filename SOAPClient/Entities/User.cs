using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeasuresAdvanticMiddlewareDownloader.Entities
{
    public class User
    {
        #region Public Virtual Properties

        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Description { get; set; }
       
        #endregion

        #region const properties
        // Appliance TABLE

        const int DEFAULT_ID = 1;
        const String DEFAULT_USERNAME = "Gnarum_ADmOr2!";
        const String DEFAULT_DESCRIPTION="Gnarum";

        #endregion

        #region constructor

        public User(String userName)
        {
            Id = DEFAULT_ID;
            UserName = userName;
            Description = DEFAULT_DESCRIPTION;
        }

        public User(int id, String userName, String description)
        {
            Id = id;
            UserName = userName;
            Description = description;
        }
        public User()
        {
            Id = DEFAULT_ID;
            UserName = DEFAULT_USERNAME;
            Description = DEFAULT_DESCRIPTION;
        }

        #endregion



        #region Public Override Methods

        public override bool Equals(object obj)
        {
            User u = obj as User;
            return u != null
                && u.UserName == UserName;
        }

        public override int GetHashCode()
        {
            return UserName.GetHashCode() + 5669;
        }

        #endregion
    }
}
