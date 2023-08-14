using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Challenges.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class propnamefixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChallengeRequests_Survey_SurveyId",
                schema: "challenge",
                table: "ChallengeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Survey_SurveyTypes_SurveyTypeId",
                schema: "challenge",
                table: "Survey");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyGenre_Genres_GenreId",
                schema: "challenge",
                table: "SurveyGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyGenre_Survey_SurveyId",
                schema: "challenge",
                table: "SurveyGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestion_Questions_QuestionId",
                schema: "challenge",
                table: "SurveyQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestion_Survey_SurveyId",
                schema: "challenge",
                table: "SurveyQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyTags_Survey_SurveyId",
                schema: "challenge",
                table: "SurveyTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyQuestion",
                schema: "challenge",
                table: "SurveyQuestion");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyGenre",
                schema: "challenge",
                table: "SurveyGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Survey",
                schema: "challenge",
                table: "Survey");

            migrationBuilder.RenameTable(
                name: "SurveyQuestion",
                schema: "challenge",
                newName: "SurveyQuestions",
                newSchema: "challenge");

            migrationBuilder.RenameTable(
                name: "SurveyGenre",
                schema: "challenge",
                newName: "SurveyGenres",
                newSchema: "challenge");

            migrationBuilder.RenameTable(
                name: "Survey",
                schema: "challenge",
                newName: "Surveys",
                newSchema: "challenge");

            migrationBuilder.RenameColumn(
                name: "CompletedOn",
                schema: "challenge",
                table: "ChallengeRequests",
                newName: "CompletedAt");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestion_SurveyId",
                schema: "challenge",
                table: "SurveyQuestions",
                newName: "IX_SurveyQuestions_SurveyId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestion_QuestionId",
                schema: "challenge",
                table: "SurveyQuestions",
                newName: "IX_SurveyQuestions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyGenre_SurveyId",
                schema: "challenge",
                table: "SurveyGenres",
                newName: "IX_SurveyGenres_SurveyId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyGenre_GenreId",
                schema: "challenge",
                table: "SurveyGenres",
                newName: "IX_SurveyGenres_GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Survey_SurveyTypeId",
                schema: "challenge",
                table: "Surveys",
                newName: "IX_Surveys_SurveyTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyQuestions",
                schema: "challenge",
                table: "SurveyQuestions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyGenres",
                schema: "challenge",
                table: "SurveyGenres",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Surveys",
                schema: "challenge",
                table: "Surveys",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChallengeRequests_Surveys_SurveyId",
                schema: "challenge",
                table: "ChallengeRequests",
                column: "SurveyId",
                principalSchema: "challenge",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyGenres_Genres_GenreId",
                schema: "challenge",
                table: "SurveyGenres",
                column: "GenreId",
                principalSchema: "challenge",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyGenres_Surveys_SurveyId",
                schema: "challenge",
                table: "SurveyGenres",
                column: "SurveyId",
                principalSchema: "challenge",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Questions_QuestionId",
                schema: "challenge",
                table: "SurveyQuestions",
                column: "QuestionId",
                principalSchema: "challenge",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestions_Surveys_SurveyId",
                schema: "challenge",
                table: "SurveyQuestions",
                column: "SurveyId",
                principalSchema: "challenge",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_SurveyTypes_SurveyTypeId",
                schema: "challenge",
                table: "Surveys",
                column: "SurveyTypeId",
                principalSchema: "challenge",
                principalTable: "SurveyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyTags_Surveys_SurveyId",
                schema: "challenge",
                table: "SurveyTags",
                column: "SurveyId",
                principalSchema: "challenge",
                principalTable: "Surveys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChallengeRequests_Surveys_SurveyId",
                schema: "challenge",
                table: "ChallengeRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyGenres_Genres_GenreId",
                schema: "challenge",
                table: "SurveyGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyGenres_Surveys_SurveyId",
                schema: "challenge",
                table: "SurveyGenres");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Questions_QuestionId",
                schema: "challenge",
                table: "SurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyQuestions_Surveys_SurveyId",
                schema: "challenge",
                table: "SurveyQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_SurveyTypes_SurveyTypeId",
                schema: "challenge",
                table: "Surveys");

            migrationBuilder.DropForeignKey(
                name: "FK_SurveyTags_Surveys_SurveyId",
                schema: "challenge",
                table: "SurveyTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Surveys",
                schema: "challenge",
                table: "Surveys");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyQuestions",
                schema: "challenge",
                table: "SurveyQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SurveyGenres",
                schema: "challenge",
                table: "SurveyGenres");

            migrationBuilder.RenameTable(
                name: "Surveys",
                schema: "challenge",
                newName: "Survey",
                newSchema: "challenge");

            migrationBuilder.RenameTable(
                name: "SurveyQuestions",
                schema: "challenge",
                newName: "SurveyQuestion",
                newSchema: "challenge");

            migrationBuilder.RenameTable(
                name: "SurveyGenres",
                schema: "challenge",
                newName: "SurveyGenre",
                newSchema: "challenge");

            migrationBuilder.RenameColumn(
                name: "CompletedAt",
                schema: "challenge",
                table: "ChallengeRequests",
                newName: "CompletedOn");

            migrationBuilder.RenameIndex(
                name: "IX_Surveys_SurveyTypeId",
                schema: "challenge",
                table: "Survey",
                newName: "IX_Survey_SurveyTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestions_SurveyId",
                schema: "challenge",
                table: "SurveyQuestion",
                newName: "IX_SurveyQuestion_SurveyId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyQuestions_QuestionId",
                schema: "challenge",
                table: "SurveyQuestion",
                newName: "IX_SurveyQuestion_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyGenres_SurveyId",
                schema: "challenge",
                table: "SurveyGenre",
                newName: "IX_SurveyGenre_SurveyId");

            migrationBuilder.RenameIndex(
                name: "IX_SurveyGenres_GenreId",
                schema: "challenge",
                table: "SurveyGenre",
                newName: "IX_SurveyGenre_GenreId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Survey",
                schema: "challenge",
                table: "Survey",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyQuestion",
                schema: "challenge",
                table: "SurveyQuestion",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SurveyGenre",
                schema: "challenge",
                table: "SurveyGenre",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ChallengeRequests_Survey_SurveyId",
                schema: "challenge",
                table: "ChallengeRequests",
                column: "SurveyId",
                principalSchema: "challenge",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Survey_SurveyTypes_SurveyTypeId",
                schema: "challenge",
                table: "Survey",
                column: "SurveyTypeId",
                principalSchema: "challenge",
                principalTable: "SurveyTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyGenre_Genres_GenreId",
                schema: "challenge",
                table: "SurveyGenre",
                column: "GenreId",
                principalSchema: "challenge",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyGenre_Survey_SurveyId",
                schema: "challenge",
                table: "SurveyGenre",
                column: "SurveyId",
                principalSchema: "challenge",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestion_Questions_QuestionId",
                schema: "challenge",
                table: "SurveyQuestion",
                column: "QuestionId",
                principalSchema: "challenge",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyQuestion_Survey_SurveyId",
                schema: "challenge",
                table: "SurveyQuestion",
                column: "SurveyId",
                principalSchema: "challenge",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SurveyTags_Survey_SurveyId",
                schema: "challenge",
                table: "SurveyTags",
                column: "SurveyId",
                principalSchema: "challenge",
                principalTable: "Survey",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
