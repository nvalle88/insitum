using insitum.Utiles;
using Quartz;
using Quartz.Impl;
using System;

namespace ScheduledTaskExample.ScheduledTasks
{
    public class JobScheduler
    {
        public static void Start()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<EmailJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                  (s =>
                     s.WithIntervalInMinutes(Constantes.TiempoCicloMinutosNotificacionEmail)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(Constantes.HoraInicioCiclo,Constantes.MinutoInicioCiclo))
                  )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}