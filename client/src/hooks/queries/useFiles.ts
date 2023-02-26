import { useQuery } from 'react-query';
import { getFiles } from '@/services/file';

export const FILES_QUERY_KEY = "FILES_QUERY_KEY";

const useFiles = () => {
    return useQuery({
        queryKey: FILES_QUERY_KEY,
        queryFn: getFiles,
        refetchOnMount: false,
        refetchOnWindowFocus: false,
        refetchOnReconnect: true,
    })
};

export default useFiles;
