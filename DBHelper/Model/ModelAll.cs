using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBHelper.Model
{

    /// <summary>
    /// 题目类型
    /// </summary>
    public struct TitleType
    {
        public int TiteTypeId;
        public string TitleTypeName;
    }

    /// <summary>
    /// 题目与选项关系表
    /// </summary>
    public struct TitleItemAssoc
    {
        public int TitleItemAssocId;
        public int TitleInfoId;
        public int TitleItemId;
    }

    /// <summary>
    /// 题目选项
    /// </summary>
    public struct TitleItem
    {
        public int TitleItemId;
        public string TitleItemContent;
    }

    /// <summary>
    /// 题目信息
    /// </summary>
    public struct TitleInfo
    {
        public int TitleInfoId;
        public string TitleConent;
        /// <summary>
        /// 题目类型(选择或判断)
        /// </summary>
        public int TitleTypeId;
        /// <summary>
        /// 题目类别()
        /// </summary>
        public int TypeId;
        public int CorrectAnswer;
        public double Score;

    }

    /// <summary>
    /// 学生考试答题详情
    /// </summary>
    public struct StudentExaminationPaper
    {
        public int StudentExaminationPaperId;
        /// <summary>
        /// 学生编号
        /// </summary>
        public int StudentId;
        /// <summary>
        /// 题目编号
        /// </summary>
        public int TitleInfoId;
        /// <summary>
        /// 学生写的答案
        /// </summary>
        public int StudentAnswer;
        /// <summary>
        /// 得分
        /// </summary>
        public double Score;
        /// <summary>
        /// 考试Id
        /// </summary>
        public int ExercisesTestId;
    }

    public struct StudentExam
    {
        /// <summary>
        /// 
        /// </summary>
        public int StudentExamId;
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentName;
        /// <summary>
        /// 学号
        /// </summary>
        public string StudentNumber;
        /// <summary>
        /// 性别
        /// </summary>
        public int StudentSex;
        /// <summary>
        /// 电话号码
        /// </summary>
        public string StudentPhone;
        /// <summary>
        /// 总分
        /// </summary>
        public double TotalScore;
    }

    /// <summary>
    /// 实验类型
    /// </summary>
    public struct ExperimentType
    {
        public int TypeId;
        /// <summary>
        /// 基础认知等
        /// </summary>
        public string TypeName1;
        /// <summary>
        /// 
        /// </summary>
        public string TypeName2;
        public string TypeDescribe;
    }
    /// <summary>
    /// 练习、考试与题目对照表
    /// </summary>
    public struct ExercisesTitle
    {
        public int ExercisesTitleId;
        /// <summary>
        /// 题目ID
        /// </summary>
        public int TitleInfoId;
        /// <summary>
        /// 考试ID
        /// </summary>
        public int ExercisesTestId;
    }

    /// <summary>
    /// 习题或考试表
    /// </summary>
    public struct ExercisesTest
    {
        public int ExercisesTestId;
        /// <summary>
        /// 考试名称
        /// </summary>
        public string ExercisesName;
        /// <summary>
        /// 考试描述
        /// </summary>
        public string ExercisesDescribe;
        /// <summary>
        /// -1:表示一次考试。其它表示一次习题，与ExperimentType表中的TypeId相关联
        /// </summary>
        public int ExercisesTypeId;
    }
}
