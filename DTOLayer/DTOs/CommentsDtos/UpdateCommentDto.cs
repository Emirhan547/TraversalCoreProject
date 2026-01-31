using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOLayer.DTOs.CommentsDtos
{
    public class UpdateCommentDto
    {
        public int Id { get; set; }
        public string CommentUser { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; }
        public bool CommentState { get; set; }
        public int DestinationId { get; set; }
        public int AppUserId { get; set; }
    }
}
