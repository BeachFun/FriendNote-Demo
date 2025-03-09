namespace FriendNote
{
    internal static class Notices
    {
        internal static class StartupNotice
        {
            internal static string ALL_SERVICES_STARTED { get; private set; } = "ALL_MANAGERS_STARTED";
            internal static string MANAGERS_PROGRESS { get; private set; } = "MANAGERS_PROGRESS";

            internal static string ALL_CONTROLLERS_STARTED { get; private set; } = "ALL_CONTROLLERS_STARTED";
            internal static string CONTROLLERS_PROGRESS { get; private set; } = "CONTROLLERS_PROGRESS";

            internal static string PLAYER_MANAGER_STARTING { get; private set; } = "PLAYER_MANAGER_STARTING";
            internal static string DATA_MANAGER_STARTING { get; private set; } = "DATA_MANAGER_STARTING";
            internal static string GAME_MANAGER_STARTING { get; private set; } = "GAME_MANAGER_STARTING";
            internal static string UI_MANAGER_STARTING { get; private set; } = "UI_MANAGER_STARTING";
            internal static string SETINGS_MANAGER_STARTING { get; private set; } = "SETINGS_MANAGER_STARTING";

            internal static string PLAYER_MANAGER_STARTED { get; private set; } = "PLAYER_MANAGER_STARTED";
            internal static string DATA_MANAGER_STARTED { get; private set; } = "DATA_MANAGER_STARTED";
            internal static string GAME_MANAGER_STARTED { get; private set; } = "GAME_MANAGER_STARTED";
            internal static string UI_MANAGER_STARTED { get; private set; } = "UI_MANAGER_STARTED";
            internal static string SETINGS_MANAGER_STARTED { get; private set; } = "SETINGS_MANAGER_STARTED";

            internal static string CURRENCY_CONTROLLER_STARTED { get; private set; } = "CURRENCY_CONTROLLER_STARTED";
            internal static string STOCK_EXCHANGE_CONTROLLER_STARTED { get; private set; } = "STOCK_EXCHANGE_CONTROLLER_STARTED";

            internal static string PRIMARY_PRICES_GENERATION { get; private set; } = "PRIMARY_PRICES_GENERATION";

            internal static string GAME_STARTED { get; private set; } = "STARTUP_NOTICE_" + "GAME_STARTED";
        }

        internal static class DataNotice
        {
            internal static string PERSONS_DATA_UPDATED { get; private set; } = "DATA_NOTICE_" + "PERSONS_DATA_UPDATED";
            internal static string PERSONS_PERSONINFO_DATA_UPDATED { get; private set; } = "DATA_NOTICE_" + "PERSONS_PERSONINFO_DATA_UPDATED";
            internal static string PERSONS_RESIDENCES_DATA_UPDATED { get; private set; } = "DATA_NOTICE_" + "PERSONS_RESIDENCES_DATA_UPDATED";
            internal static string PERSONS_EDUCATION_DATA_UPDATED { get; private set; } = "DATA_NOTICE_" + "PERSONS_EDUCATION_DATA_UPDATED";
            internal static string PERSONS_CAREER_DATA_UPDATED { get; private set; } = "DATA_NOTICE_" + "PERSONS_CAREER_DATA_UPDATED";
            internal static string PERSONS_SKILLS_DATA_UPDATED { get; private set; } = "DATA_NOTICE_" + "PERSONS_SKILLS_DATA_UPDATED";
            internal static string PERSONS_INTERESTS_DATA_UPDATED { get; private set; } = "DATA_NOTICE_" + "PERSONS_INTERESTS_DATA_UPDATED";
            internal static string PERSONS_GOALS_DATA_UPDATED { get; private set; } = "DATA_NOTICE_" + "PERSONS_GOALS_DATA_UPDATED";

            internal static string MODEL_UPDATED_OLD { get; private set; } = "DATA_NOTICE_" + "MODEL_UPDATED"; // TODO: удалить устаревшее
        }

        internal static class UINotice
        {
            internal static string EDIT_PAGE_PERSON_EDUCATION_CLOSED { get; private set; } = "UI_NOTICE_" + "EDIT_PAGE_PERSON_EDUCATION_CLOSED";
            internal static string EDIT_PAGE_PERSON_GOAL_CLOSED { get; private set; } = "UI_NOTICE_" + "EDIT_PAGE_PERSON_GOAL_CLOSED";
            internal static string EDIT_PAGE_PERSON_PROFILE_CLOSED { get; private set; } = "UI_NOTICE_" + "EDIT_PAGE_PERSON_INFO_CLOSED";
            internal static string EDIT_PAGE_PERSON_INTEREST_CLOSED { get; private set; } = "UI_NOTICE_" + "EDIT_PAGE_PERSON_INTEREST_CLOSED";
            internal static string EDIT_PAGE_PERSON_RESIDENCE_CLOSED { get; private set; } = "UI_NOTICE_" + "EDIT_PAGE_PERSON_RESIDENCE_CLOSED";
            internal static string EDIT_PAGE_PERSON_SKILL_CLOSED { get; private set; } = "UI_NOTICE_" + "EDIT_PAGE_PERSON_SKILL_CLOSED";
            internal static string EDIT_PAGE_PERSON_WORK_POSITION_CLOSED { get; private set; } = "UI_NOTICE_" + "EDIT_PAGE_PERSON_WORK_POSITION_CLOSED";

            internal static string INFO_PAGE_PERSON_EDUCATION_CLOSED { get; private set; } = "UI_NOTICE_" + "INFO_PAGE_PERSON_EDUCATION_CLOSED";
            internal static string INFO_PAGE_PERSON_GOAL_CLOSED { get; private set; } = "UI_NOTICE_" + "INFO_PAGE_PERSON_GOAL_CLOSED";
            internal static string INFO_PAGE_PERSON_PROFILE_CLOSED { get; private set; } = "UI_NOTICE_" + "INFO_PAGE_PERSON_INFO_CLOSED";
            internal static string INFO_PAGE_PERSON_INTEREST_CLOSED { get; private set; } = "UI_NOTICE_" + "INFO_PAGE_PERSON_INTEREST_CLOSED";
            internal static string INFO_PAGE_PERSON_RESIDENCE_CLOSED { get; private set; } = "UI_NOTICE_" + "INFO_PAGE_PERSON_RESIDENCE_CLOSED";
            internal static string INFO_PAGE_PERSON_SKILL_CLOSED { get; private set; } = "UI_NOTICE_" + "INFO_PAGE_PERSON_SKILL_CLOSED";
            internal static string INFO_PAGE_PERSON_WORK_POSITION_CLOSED { get; private set; } = "UI_NOTICE_" + "INFO_PAGE_PERSON_WORK_POSITION_CLOSED";
        }
    }
}
