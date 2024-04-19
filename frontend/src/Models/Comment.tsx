export type CommentGet = {
  id: number;
  content: string;
  creator: {
    email: string;
    firstName: string;
    lastName: string;
  };
};

export type CommentPost = {
  comment: string;
  issueKey: string;
};
