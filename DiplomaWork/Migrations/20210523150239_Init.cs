using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiplomaWork1.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ServiceId = table.Column<Guid>(nullable: false),
                    UserEmail = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    UserPhone = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CurrentEmployeeId = table.Column<Guid>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsCompleted = table.Column<bool>(nullable: false),
                    IsReturned = table.Column<bool>(nullable: false),
                    IsWaitingQualityControl = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_requests_users_CurrentEmployeeId",
                        column: x => x.CurrentEmployeeId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_requests_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "userservices",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    ServiceId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_userservices", x => new { x.UserId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_userservices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_userservices_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "requeststates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RequestId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    State = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requeststates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_requeststates_requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_requeststates_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("368fbf27-d4b9-4400-963d-f9ab5dd40894"), "Телефоны" },
                    { new Guid("a6683656-cc29-44f6-863d-8ee1ee915db1"), "Ноутбуки" },
                    { new Guid("fe6bf3db-2b81-491c-9269-0335ceaa4297"), "Принтеры" }
                });

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("ffce5e46-7143-454d-9d8b-9b7ba5bc5488"), "a5336036-976d-4611-ad8d-27bf11d3c717", "Admin", "ADMIN" },
                    { new Guid("1ddea4f0-3ce3-40e2-a668-8281fbce96c2"), "bbdc4c3b-1a7e-4a64-8e85-47f1eb94e0be", "Отдел кадров", "ОТДЕЛ КАДРОВ" },
                    { new Guid("ccc481e1-739c-474c-a64d-432244fce57d"), "5dfc555b-6bfe-442c-9846-1c3c2ca7d582", "Технических специалист", "ТЕХНИЧЕСКИХ СПЕЦИАЛИСТ" },
                    { new Guid("4f55cdd8-6c38-43ee-89c2-8d89b7680f0e"), "a8d1f88f-99b7-4ed4-82cb-f6cc5e91b472", "Контроль качества", "КОНТРОЛЬ КАЧЕСТВА" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("60b56aac-db99-4ebe-b828-2b4ba40ae15b"), new Guid("368fbf27-d4b9-4400-963d-f9ab5dd40894"), "Другое" },
                    { new Guid("66d89e49-d6be-4120-85f2-a1a21736c709"), new Guid("368fbf27-d4b9-4400-963d-f9ab5dd40894"), "Замена экрана" },
                    { new Guid("2e775478-eb40-4fea-aafa-261e6f6db1fb"), new Guid("368fbf27-d4b9-4400-963d-f9ab5dd40894"), "Замена аккумулятора" },
                    { new Guid("be42a735-12c9-4237-9637-ec4bd3073d98"), new Guid("a6683656-cc29-44f6-863d-8ee1ee915db1"), "Другое" },
                    { new Guid("b11d6469-2a3b-4b8d-86ff-a5a9861e9384"), new Guid("a6683656-cc29-44f6-863d-8ee1ee915db1"), "Проблема с клавиатурой" },
                    { new Guid("52347af0-0366-441b-b881-268924d4fa04"), new Guid("a6683656-cc29-44f6-863d-8ee1ee915db1"), "Замена матрицы" },
                    { new Guid("21c474f2-47e1-41c5-b755-c2e47c4694ca"), new Guid("a6683656-cc29-44f6-863d-8ee1ee915db1"), "Чистка компьютера" },
                    { new Guid("3c88e7cd-8756-4aa1-a098-787caefcb3a5"), new Guid("fe6bf3db-2b81-491c-9269-0335ceaa4297"), "Другое" },
                    { new Guid("6edeeec0-e147-47d4-9bbe-3db9938b0d75"), new Guid("fe6bf3db-2b81-491c-9269-0335ceaa4297"), "Заправка картреджа" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_requests_CurrentEmployeeId",
                table: "requests",
                column: "CurrentEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_requests_ServiceId",
                table: "requests",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_requeststates_RequestId",
                table: "requeststates",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_requeststates_UserId",
                table: "requeststates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_CategoryId",
                table: "Services",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_Name_CategoryId",
                table: "Services",
                columns: new[] { "Name", "CategoryId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "users",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_userservices_ServiceId",
                table: "userservices",
                column: "ServiceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "requeststates");

            migrationBuilder.DropTable(
                name: "userservices");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "requests");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
