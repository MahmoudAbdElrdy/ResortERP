using AutoMapper;
using ResortERP.Core;
using ResortERP.Core.VM;
using System;
using System.Globalization;

namespace ResortERP.Api.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //DashBorad Items
            Mapper.CreateMap<User, UserVM>()
           .ForMember(c => c.COMPANY_LOGO, opt => opt.Ignore());
            Mapper.CreateMap<UserVM, User>()
           .ForMember(c => c.COMPANY_LOGO, opt => opt.Ignore());

            Mapper.CreateMap<NotificationType, NotificationTypeVM>()
           .ForMember(c => c.NAME, opt => opt.Ignore());
            Mapper.CreateMap<NotificationTypeVM, NotificationType>()
            .ForMember(c => c.ID, opt => opt.Ignore());

            Mapper.CreateMap<Notification, NotificationVM>()
           .ForMember(c => c.Body, opt => opt.Ignore());
            Mapper.CreateMap<NotificationVM, Notification>()
           .ForMember(c => c.ID, opt => opt.Ignore());

            Mapper.CreateMap<NotificationsView, NotificationsViewVM>()
           .ForMember(c => c.Body, opt => opt.Ignore())
           .ForMember(c => c.FromUserFullName, opt => opt.Ignore())
           .ForMember(c => c.ToUserFullName, opt => opt.Ignore());

            Mapper.CreateMap<NotificationsViewVM, NotificationsView>()
           .ForMember(c => c.NID, opt => opt.Ignore());

            Mapper.CreateMap<Message, MessageVM>()
           .ForMember(c => c.Body, opt => opt.Ignore());
            Mapper.CreateMap<MessageVM, Message>()
           .ForMember(c => c.ID, opt => opt.Ignore());

            Mapper.CreateMap<MessagesView, MessagesViewVM>()
           .ForMember(c => c.MessageBody, opt => opt.Ignore())
           .ForMember(c => c.FromUserFullName, opt => opt.Ignore())
           .ForMember(c => c.ToUserFullName, opt => opt.Ignore());
            Mapper.CreateMap<MessagesViewVM, MessagesView>()
            .ForMember(c => c.NID, opt => opt.Ignore());
            //UserPrivilages Mapping
            Mapper.CreateMap<UserPrivilige, UserPriviligeVM>();
            Mapper.CreateMap<UserPriviligeVM, UserPrivilige>();

            //user Menu Mapping
            Mapper.CreateMap<UserMenu, UserMenuVM>();
            Mapper.CreateMap<UserMenuVM, UserMenu>()
            .ForMember(c => c.ID, opt => opt.Ignore());

            //ORDERS Mapping
            Mapper.CreateMap<ORDERS, OrdersVM>();
            Mapper.CreateMap<OrdersVM, ORDERS>();

            //SHORTCUTS Mapping
            Mapper.CreateMap<SHORTCUTS, ShortcutsVM>();
            Mapper.CreateMap<ShortcutsVM, SHORTCUTS>();

            //Emails Mapping
            Mapper.CreateMap<Emails, EmailsVM>();
            Mapper.CreateMap<EmailsVM, Emails>();

            //SYSTEM_OPTIONS Mapping
            Mapper.CreateMap<SYSTEM_OPTIONS, System_OptionsVM>();
            Mapper.CreateMap<System_OptionsVM, SYSTEM_OPTIONS>();

            //TBOXACCOUNTS Mapping
            Mapper.CreateMap<TBOXACCOUNTS, TBoxAccountsVM>();
            Mapper.CreateMap<TBoxAccountsVM, TBOXACCOUNTS>();

            //TSTORE Mapping
            Mapper.CreateMap<TSTORE, TStoreVM>();
            Mapper.CreateMap<TStoreVM, TSTORE>();

            //TBUDGETACCOUNTS Mapping
            Mapper.CreateMap<TBUDGETACCOUNTS, TBudgetAccountsVM>();
            Mapper.CreateMap<TBudgetAccountsVM, TBUDGETACCOUNTS>();
        }
    }
}