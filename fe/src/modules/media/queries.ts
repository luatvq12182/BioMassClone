import { useQuery } from 'react-query';
import { getMedia } from './services';

export const MEDIA_QUERY_KEY = "MEDIA_QUERY_KEY";

const useMedia = () => {
    return useQuery({
        queryKey: MEDIA_QUERY_KEY,
        queryFn: getMedia,
        refetchOnMount: false,
        refetchOnWindowFocus: false,
        refetchOnReconnect: true,
    })
};

export default useMedia;