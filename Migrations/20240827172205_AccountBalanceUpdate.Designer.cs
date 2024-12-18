﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using elementium_backend;

#nullable disable

namespace elementium_backend.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240827172205_AccountBalanceUpdate")]
    partial class AccountBalanceUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("elementium_backend.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("AccountId"));

                    b.Property<int>("AccountStatusId")
                        .HasColumnType("integer");

                    b.Property<bool>("Active")
                        .HasColumnType("boolean");

                    b.Property<int>("Balance_h2")
                        .HasColumnType("integer");

                    b.Property<int>("Balance_li")
                        .HasColumnType("integer");

                    b.Property<int>("Balance_pd")
                        .HasColumnType("integer");

                    b.Property<int>("Balance_xe")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("AccountId");

                    b.HasIndex("AccountStatusId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("elementium_backend.AuthenticationLog", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LogId"));

                    b.Property<int>("DeviceInfo")
                        .HasColumnType("integer");

                    b.Property<int>("IpAddress")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("LoginTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("LogoutTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("LogId");

                    b.HasIndex("UserId");

                    b.ToTable("AuthenticationLogs");
                });

            modelBuilder.Entity("elementium_backend.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("StatusId"));

                    b.Property<float>("Annual_interest_rate")
                        .HasColumnType("real");

                    b.Property<string>("Status_name")
                        .HasColumnType("text");

                    b.Property<int>("Total_amount_criteria")
                        .HasColumnType("integer");

                    b.Property<float>("Transaction_fee")
                        .HasColumnType("real");

                    b.Property<string>("Transactions_criteria")
                        .HasColumnType("text");

                    b.HasKey("StatusId");

                    b.ToTable("status");
                });

            modelBuilder.Entity("elementium_backend.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TransactionId"));

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("FromAccountId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("Timestamp")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("ToAccountId")
                        .HasColumnType("integer");

                    b.Property<string>("TransactionType")
                        .HasColumnType("text");

                    b.HasKey("TransactionId");

                    b.HasIndex("FromAccountId");

                    b.HasIndex("ToAccountId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("elementium_backend.UserSecurity", b =>
                {
                    b.Property<int>("SecurityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("SecurityId"));

                    b.Property<string>("Latest_otp_secret")
                        .HasColumnType("text");

                    b.Property<string>("Password_hash")
                        .HasColumnType("text");

                    b.Property<DateTime?>("Uploaded_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("SecurityId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("user_security");
                });

            modelBuilder.Entity("elementium_backend.Users", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserId"));

                    b.Property<string>("Created_at")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("elementium_backend.Account", b =>
                {
                    b.HasOne("elementium_backend.Status", "Status")
                        .WithMany("Accounts")
                        .HasForeignKey("AccountStatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("elementium_backend.Users", "User")
                        .WithOne("Account")
                        .HasForeignKey("elementium_backend.Account", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Status");

                    b.Navigation("User");
                });

            modelBuilder.Entity("elementium_backend.AuthenticationLog", b =>
                {
                    b.HasOne("elementium_backend.Users", "User")
                        .WithMany("AuthenticationLog")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("elementium_backend.Transaction", b =>
                {
                    b.HasOne("elementium_backend.Account", "FromAccount")
                        .WithMany("FromTransactions")
                        .HasForeignKey("FromAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("elementium_backend.Account", "ToAccount")
                        .WithMany("ToTransactions")
                        .HasForeignKey("ToAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FromAccount");

                    b.Navigation("ToAccount");
                });

            modelBuilder.Entity("elementium_backend.UserSecurity", b =>
                {
                    b.HasOne("elementium_backend.Users", "Users")
                        .WithOne("UserSecurity")
                        .HasForeignKey("elementium_backend.UserSecurity", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Users");
                });

            modelBuilder.Entity("elementium_backend.Account", b =>
                {
                    b.Navigation("FromTransactions");

                    b.Navigation("ToTransactions");
                });

            modelBuilder.Entity("elementium_backend.Status", b =>
                {
                    b.Navigation("Accounts");
                });

            modelBuilder.Entity("elementium_backend.Users", b =>
                {
                    b.Navigation("Account");

                    b.Navigation("AuthenticationLog");

                    b.Navigation("UserSecurity");
                });
#pragma warning restore 612, 618
        }
    }
}
