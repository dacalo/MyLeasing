using Microsoft.EntityFrameworkCore.Migrations;

namespace MyLeasing.Web.Migrations
{
    public partial class CompleteDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Lessee_LesseeId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Owners_OwnerId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Contract_Property_PropertyId",
                table: "Contract");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_Owners_OwnerId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_Property_PropertyType_PropertyTypeId",
                table: "Property");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyImage_Property_PropertyId",
                table: "PropertyImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyType",
                table: "PropertyType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyImage",
                table: "PropertyImage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Property",
                table: "Property");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessee",
                table: "Lessee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contract",
                table: "Contract");

            migrationBuilder.RenameTable(
                name: "PropertyType",
                newName: "PropertyTypes");

            migrationBuilder.RenameTable(
                name: "PropertyImage",
                newName: "PropertyImages");

            migrationBuilder.RenameTable(
                name: "Property",
                newName: "Properties");

            migrationBuilder.RenameTable(
                name: "Lessee",
                newName: "Lessees");

            migrationBuilder.RenameTable(
                name: "Contract",
                newName: "Contracts");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyImage_PropertyId",
                table: "PropertyImages",
                newName: "IX_PropertyImages_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_PropertyTypeId",
                table: "Properties",
                newName: "IX_Properties_PropertyTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Property_OwnerId",
                table: "Properties",
                newName: "IX_Properties_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_PropertyId",
                table: "Contracts",
                newName: "IX_Contracts_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_OwnerId",
                table: "Contracts",
                newName: "IX_Contracts_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Contract_LesseeId",
                table: "Contracts",
                newName: "IX_Contracts_LesseeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyTypes",
                table: "PropertyTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyImages",
                table: "PropertyImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Properties",
                table: "Properties",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessees",
                table: "Lessees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Lessees_LesseeId",
                table: "Contracts",
                column: "LesseeId",
                principalTable: "Lessees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Owners_OwnerId",
                table: "Contracts",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Properties_PropertyId",
                table: "Contracts",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Owners_OwnerId",
                table: "Properties",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_PropertyTypes_PropertyTypeId",
                table: "Properties",
                column: "PropertyTypeId",
                principalTable: "PropertyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyImages_Properties_PropertyId",
                table: "PropertyImages",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Lessees_LesseeId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Owners_OwnerId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Properties_PropertyId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Owners_OwnerId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_PropertyTypes_PropertyTypeId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_PropertyImages_Properties_PropertyId",
                table: "PropertyImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyTypes",
                table: "PropertyTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PropertyImages",
                table: "PropertyImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Properties",
                table: "Properties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessees",
                table: "Lessees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contracts",
                table: "Contracts");

            migrationBuilder.RenameTable(
                name: "PropertyTypes",
                newName: "PropertyType");

            migrationBuilder.RenameTable(
                name: "PropertyImages",
                newName: "PropertyImage");

            migrationBuilder.RenameTable(
                name: "Properties",
                newName: "Property");

            migrationBuilder.RenameTable(
                name: "Lessees",
                newName: "Lessee");

            migrationBuilder.RenameTable(
                name: "Contracts",
                newName: "Contract");

            migrationBuilder.RenameIndex(
                name: "IX_PropertyImages_PropertyId",
                table: "PropertyImage",
                newName: "IX_PropertyImage_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_PropertyTypeId",
                table: "Property",
                newName: "IX_Property_PropertyTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Properties_OwnerId",
                table: "Property",
                newName: "IX_Property_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_PropertyId",
                table: "Contract",
                newName: "IX_Contract_PropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_OwnerId",
                table: "Contract",
                newName: "IX_Contract_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Contracts_LesseeId",
                table: "Contract",
                newName: "IX_Contract_LesseeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyType",
                table: "PropertyType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PropertyImage",
                table: "PropertyImage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Property",
                table: "Property",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessee",
                table: "Lessee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contract",
                table: "Contract",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Lessee_LesseeId",
                table: "Contract",
                column: "LesseeId",
                principalTable: "Lessee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Owners_OwnerId",
                table: "Contract",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contract_Property_PropertyId",
                table: "Contract",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_Owners_OwnerId",
                table: "Property",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Property_PropertyType_PropertyTypeId",
                table: "Property",
                column: "PropertyTypeId",
                principalTable: "PropertyType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PropertyImage_Property_PropertyId",
                table: "PropertyImage",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
