using System;
using System.Collections.Generic;
using FriendNote.Domain.DTO;

namespace FriendNote.Infrastructure
{
    public static class PageRegistry
    {
        private static readonly Dictionary<Type, string> _editPages = new()
        {
            { typeof(Education), "Edit Page - Person Education" },
            { typeof(Goal), "Edit Page - Person Goal" },
            { typeof(Interest), "Edit Page - Person Interest" },
            { typeof(PersonProfile), "Edit Page - Person Profile" },
            { typeof(Residence), "Edit Page - Person Residence" },
            { typeof(Skill), "Edit Page - Person Skill" },
            { typeof(WorkPosition), "Edit Page - Person Work Position" },
        };

        private static readonly Dictionary<Type, string> _infoPages = new()
        {
            { typeof(Education), "Page - Person Education" },
            { typeof(Goal), "Page - Person Goals" },
            { typeof(Interest), "Page - Person Interests" },
            { typeof(PersonProfile), "Page - Person Profile" },
            { typeof(Residence), "Page - Person Residences" },
            { typeof(Skill), "Page - Person Skills" },
            { typeof(WorkPosition), "Page - Person Career" }
        };

        public static string GetEditPageId<T>() where T : class
        {
            return _editPages.TryGetValue(typeof(T), out var id)
                ? id
                : throw new KeyNotFoundException($"Edit page ID not found for type {typeof(T).Name}");
        }

        public static string GetInfoPageId<T>() where T : class
        {
            return _infoPages.TryGetValue(typeof(T), out var id)
                ? id
                : throw new KeyNotFoundException($"Info page ID not found for type {typeof(T).Name}");
        }
    }
}
