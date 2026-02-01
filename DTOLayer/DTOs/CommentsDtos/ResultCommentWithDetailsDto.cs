using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.CommentsDtos
{
    public class ResultCommentWithDetailsDto
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public string CommentUser { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; }
        public bool CommentState { get; set; }
        public int DestinationId { get; set; }
        public string DestinationCity { get; set; }
        public int AppUserId { get; set; }
        public string AppUserName { get; set; }
        public string AppUserSurname { get; set; }
        public string AppUserImageUrl { get; set; }
    }
}
