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
        public int TiteTypeId { get; set; }
        public string TitleTypeName { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }

    /// <summary>
    /// 题目与选项关系表
    /// </summary>
    public struct TitleItemAssoc
    {
        public int TitleItemAssocId { get; set; }
        public int TitleInfoId { get; set; }
        public int TitleItemId { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }

    /// <summary>
    /// 题目选项
    /// </summary>
    public struct TitleItem
    {
        public int TitleItemId { get; set; }
        public string TitleItemContent { get; set; }
        public int TitleIndex { get; set; }
        public DateTime UpdateDateTime { get; set; }
    }

    /// <summary>
    /// 题目信息
    /// </summary>
    public struct TitleInfo
    {
        public int TitleInfoId { get; set; }
        public string TitleConent { get; set; }
        /// <summary>
        /// 题目类型(选择或判断)
        /// </summary>
        public int TitleTypeId { get; set; }
        /// <summary>
        /// 题目类别()
        /// </summary>
        public int TypeId { get; set; }
        public int CorrectAnswer { get; set; }
        public double Score { get; set; }
        public DateTime UpdateDateTime { get; set; }
        /// <summary>
        /// 题目的选项列表
        /// </summary>
        public List<TitleItem> ListTitleItem { get; set; }

        #region ************* 其他
        /// <summary>
        /// 题目类型名称
        /// </summary>
        public string TitleTypeName { get; set; }
        /// <summary>
        /// 大类实验名称
        /// </summary>
        public string TypeName1 { get; set; }
        /// <summary>
        /// 小类实验名称
        /// </summary>
        public string TypeName2 { get; set; }
        #endregion

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
        public int TypeId { get; set; }
        /// <summary>
        /// 基础认知等
        /// </summary>
        public string TypeName1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TypeName2 { get; set; }
        public string TypeDescribe { get; set; }
        /// <summary>
        /// 最后操作时间
        /// </summary>
        public DateTime UpdateDateTime { get; set; }
    }
    /// <summary>
    /// 练习、考试与题目对照表
    /// </summary>
    public struct ExercisesTitle
    {
        public int ExercisesTitleId { get; set; }
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
        public int ExercisesTestId { get; set; }
        /// <summary>
        /// 考试名称
        /// </summary>
        public string ExercisesName { get; set; }
        /// <summary>
        /// 考试描述
        /// </summary>
        public string ExercisesDescribe { get; set; }
        /// <summary>
        /// -1:表示一次考试。其它表示一次习题，与ExperimentType表中的TypeId相关联
        /// </summary>
        public int ExercisesTypeId { get; set; }
        /// <summary>
        /// 一次考试对应的题目列表
        /// </summary>
        public List<TitleInfo> ListTitleInfo { get; set; }

        public DateTime UpdateDateTime { get; set; }
    }


    #region **********************************************  考试 前台传到后台参数
    public struct stGetExperimentType
    {
        /// <summary>
        /// 当前第几页
        /// </summary>
        public int CurrentPage { get; set; }
        /// <summary>
        /// 每页显示个数
        /// </summary>
        public int ShowCount { get; set; }
    }

    public struct stId
    {
        public int Id { get; set; }
    }

    #endregion
}
