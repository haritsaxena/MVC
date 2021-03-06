﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicStore.Models
{
    using System.Data.Entity;

    public class MusicStoreDbInitializer : DropCreateDatabaseAlways<MusicStoreDBContext>
    {
        protected override void Seed(MusicStoreDBContext context)
        {
            context.Artists.Add(new Artist() { Name = " John Wacker" });
            context.Genres.Add(new Genre { Name = "Jazz" });
            context.Albums.Add(
                new Album
                    {
                        Artist = new Artist { Name = "Rush" },
                        Genre = new Genre { Name = "Rock" },
                        Price = 9.99m,
                        Title = "Caravan"
                    });
            base.Seed(context);
        }
    }
}