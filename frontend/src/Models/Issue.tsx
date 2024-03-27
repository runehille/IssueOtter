export type IssueGet = {
  title: string;
  key: string;
  status: string;
  description: string;
  assignee: string;
  reporter: string;
  project: string;
};

export type IssuePost = {
  title: string;
  content: string;
};
