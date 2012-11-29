using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace Stromohab_DataAccessLayer
{
    static class Tasks
    {

        internal static void AddTask(string taskName, byte[] taskData)
        {
            stromohabDevEntities db = new stromohabDevEntities();

            task taskToAdd = new task();
            taskToAdd.tName = taskName;
            taskToAdd.tData = taskData;

            db.tasks.AddObject(taskToAdd);

            db.SaveChanges();    

         }


    }
}
